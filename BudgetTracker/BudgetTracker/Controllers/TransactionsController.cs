using BudgetTracker.DTOs.Transactions;
using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Transactions;
using BudgetTracker.Models;


namespace BudgetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public TransactionsController(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
        }

        private string GetUserId() =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User ID not found in token");

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var transactions = await _transactionRepository.GetAllByUserIdAsync(userId);
            var response = transactions.Select(TransactionResponseDto.FromTransaction);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = GetUserId();
            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if (transaction is null)
                return NotFound(new { message = "Transaction not found" });

            return Ok(TransactionResponseDto.FromTransaction(transaction));
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            if (from > to)
                return BadRequest(new { message = "From date must be before To date." });

            var userId = GetUserId();
            var summary = await _transactionRepository.GetSummaryAsync(userId, from, to);

            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category is null)
                return BadRequest(new { message = "Invalid category ID" });

            var userId = GetUserId();

            // Domain model validates itself via guard clauses
            var transaction = Models.Transaction.Create(
                userId: userId,
                description: dto.Description,
                amount: dto.Amount,
                type: dto.Type,
                categoryId: dto.CategoryId
            );

            var created = await _transactionRepository.AddAsync(transaction);

            return CreatedAtAction(
                nameof(GetById),
                new {id = created.Id},
                TransactionResponseDto.FromTransaction(created)
                );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id,  [FromBody] UpdateTransactionDto dto)
        {
            var userId = GetUserId();
            var transaction = await _transactionRepository.GetByIdAsync(id, userId);

            if(transaction is null)
                return NotFound(new {message = "Transaction not found"});

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if(category is null)
                return BadRequest(new {message = "Invalid category ID"});

            // Domain model handles update logic and sets UpdatedAt
            transaction.Update(dto.Description, dto.Amount, dto.CategoryId);

            var updated = await _transactionRepository.UpdateAsync(transaction);
            return Ok(TransactionResponseDto.FromTransaction(updated!));
        }

        // DELETE api/transactions/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var deleted = await _transactionRepository.DeleteAsync(id, userId);

            if (!deleted)
                return NotFound(new { message = "Transaction not found." });

            return NoContent(); // 204 — success with no body
        }
    }
}
