using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.DTOs.Transactions
{
    public class UpdateTransactionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
