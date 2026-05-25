using System.Net.Http.Headers;

namespace BudgetTracker.Models
{
    public enum TransactionType
    { 
        Income,
        Expense
    }

    public class Transaction
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; } //ties transaction to a user
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public Guid CategoryId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set;  } //null until edited

        private Transaction() { }


        public static Transaction Create( string userId,  string description,  decimal amount, TransactionType type, Guid categoryId )
        {
            if(string.IsNullOrWhiteSpace( userId ))
                throw new ArgumentException("UserId is required", nameof(userId));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required", nameof(description));

            if (amount <= 0 || amount > 10000)
                throw new ArgumentException("Invalid Amount", nameof(amount));

            return new Transaction
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Description = description,
                Amount = amount,
                Type = type,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(string description, decimal amount, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required", nameof(description));

            if (amount <= 0 || amount > 10000)
                throw new ArgumentException("Invalid Amount", nameof(amount));

            Description = description;
            Amount = amount;
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal SignedAmount => Type == TransactionType.Income ? Amount : -Amount;
    }
}
