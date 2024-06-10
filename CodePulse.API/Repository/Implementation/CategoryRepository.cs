using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodePulse.API.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateOrUpdateAsync(Category category)
        {
           

            if (category.Id == Guid.Empty)
            {
                category.Id = Guid.NewGuid();
                await _context.Categories.AddAsync(category);
            }
            else
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
                if (existingCategory is not null) 
                {
                    existingCategory.Name = category.Name;
                    existingCategory.UrlHandle = category.UrlHandle;
                    _context.Categories.Update(existingCategory);
                }
                else
                {
                    throw new Exception("Categoy not found");
                }
            }
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existCategory is null)
            {
                return null;
            }
            _context.Categories.Remove(existCategory);
            await _context.SaveChangesAsync();
            return existCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
           return await _context.Categories.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
