using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;

namespace CodePulse.API.Repository.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateOrUpdateAsync(BlogPost model);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> DeleteAsync(Guid id);

    }
}
