using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;

namespace CodePulse.API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateOrUpdateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetCategoryById(Guid id);
        Task<Category?> DeleteAsync(Guid id);
    }
}
