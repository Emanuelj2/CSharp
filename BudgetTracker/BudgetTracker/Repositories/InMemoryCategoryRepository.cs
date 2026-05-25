using BudgetTracker.Models;
using FluentValidation.Validators;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace BudgetTracker.Repositories
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly ConcurrentDictionary<Guid, Category> _categories = new();

        public InMemoryCategoryRepository()
        {
            // Seed some default categories on startup
            SeedDefaults();
        }

        public Task<Category> AddAscyn(Category category)
        {
            _categories[category.Id] = category;
            return Task.FromResult(category);
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Category>>(_categories.Values);
        }

        public Task<Category?> GetByIdAsync(Guid id)
        {
            _categories.TryGetValue(id, out var category);
            return Task.FromResult<Category?>(category);
        }

        private void SeedDefaults()
        {
            var defaults = new[]
            {
            Category.Create("Food & Dining", "#FF6B6B"),
            Category.Create("Transport",     "#4ECDC4"),
            Category.Create("Housing",       "#45B7D1"),
            Category.Create("Salary",        "#96CEB4"),
            Category.Create("Entertainment", "#FFEAA7"),
            Category.Create("Healthcare",    "#DDA0DD")
        };

            foreach (var category in defaults)
                _categories[category.Id] = category;
        }
    }
}
