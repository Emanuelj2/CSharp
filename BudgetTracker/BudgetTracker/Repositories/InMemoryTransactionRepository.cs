using BudgetTracker.Models;
using System.Collections.Concurrent;

namespace BudgetTracker.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        // ConcurrentDictionary is thread-safe — important for load balancing
        // where multiple requests hit the same instance simultaneously
        private readonly ConcurrentDictionary<Guid, Transaction> _transactions = new();

        public Task<IEnumerable<Transaction>> GetAllByUserIdAsync(string userId)
        {
            var result = _transactions.Values
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt);

            return Task.FromResult<IEnumerable<Transaction>>(result);
        }

        public Task<Transaction?> GetByIdAsync(Guid id, string userId)
        {
            _transactions.TryGetValue(id, out var transaction);

            // Extra security check — even if found, must belong to this user
            if (transaction?.UserId != userId)
                return Task.FromResult<Transaction?>(null);

            return Task.FromResult<Transaction?>(transaction);
        }

        public Task<Transaction> AddAsync(Transaction transaction)
        {
            _transactions[transaction.Id] = transaction;
            return Task.FromResult(transaction);
        }

        public Task<Transaction?> UpdateAsync(Transaction transaction)
        {
            // TryGetValue first to confirm it exists
            if (!_transactions.ContainsKey(transaction.Id))
                return Task.FromResult<Transaction?>(null);

            _transactions[transaction.Id] = transaction;
            return Task.FromResult<Transaction?>(transaction);
        }

        public Task<bool> DeleteAsync(Guid id, string userId)
        {
            // Only delete if it belongs to this user
            if (_transactions.TryGetValue(id, out var transaction)
                && transaction.UserId == userId)
            {
                return Task.FromResult(_transactions.TryRemove(id, out _));
            }

            return Task.FromResult(false);
        }

        public Task<BudgetSummary> GetSummaryAsync(string userId, DateTime from, DateTime to)
        {
            var userTransactions = _transactions.Values
                .Where(t => t.UserId == userId
                         && t.CreatedAt >= from
                         && t.CreatedAt <= to);

            var totalIncome = userTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            var totalExpenses = userTransactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var summary = new BudgetSummary(totalIncome, totalExpenses, from, to);
            return Task.FromResult(summary);
        }

    }
}
