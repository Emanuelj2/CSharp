using BudgetTracker.Models;

namespace BudgetTracker.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllByUserIdAsync(string  userId);
        Task<Transaction?> GetByIdAsync(Guid id, string userId);
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync (Transaction transaction);
        Task<bool> DeleteAsync (Guid id, string userId);
        Task<BudgetSummary> GetSummaryAsync (string userId, DateTime from, DateTime to);

    }
}
