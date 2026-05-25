using BudgetTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.DTOs.Transactions
{
    public class CreateTransactionDto
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        [EnumDataType(typeof(TransactionType), ErrorMessage = "Invalid input")]
        public TransactionType Type { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}