using BudgetTracker.Models;

namespace BudgetTracker.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> AddAscyn(Category category);
    }
}
