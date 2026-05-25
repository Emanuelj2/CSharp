using BudgetTracker.Models;

namespace BudgetTracker.DTOs.Transactions
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal SignedAmount { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static TransactionResponseDto FromTransaction(Transaction t) => new()
        {
            Id              = t.Id,
            Description     = t.Description,
            Amount          = t.Amount,
            SignedAmount    = t.SignedAmount,
            Type            = t.Type,
            CategoryId      = t.CategoryId,
            CreatedAt       = t.CreatedAt,
            UpdatedAt       = t.UpdatedAt
        };
    }
}
