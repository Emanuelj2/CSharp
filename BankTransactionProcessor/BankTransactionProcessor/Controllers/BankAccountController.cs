using BankTransactionProcessor.Modles.DTOs;
using BankTransactionProcessor.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankTransactionProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : Controller
    {
        private readonly BankService _bankService;
        public BankAccountController(BankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet("{accountId}/balance")]
        public async Task<IActionResult> GetBalance(int accountId)
        {
            try
            {
                var balance = await _bankService.GetBalace(accountId);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDto request)
        {
            try
            {
                var newBalance = await _bankService.Deposit(request.AccountId, request.Amount);
                return Ok(newBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDto request)
        {
            try
            {
                var newBalance = await _bankService.Withdraw(request.AccountId, request.Amount);
                return Ok(newBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
