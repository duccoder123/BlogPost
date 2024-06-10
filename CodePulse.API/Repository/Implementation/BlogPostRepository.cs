using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repository.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly AppDbContext _context;
        public BlogPostRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BlogPost> CreateOrUpdateAsync(BlogPost model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                await _context.BlogPosts.AddAsync(model);
            }
            else
            {
                var existingBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (existingBlogPost is not null) { 
                    existingBlogPost.Title = model.Title;
                    existingBlogPost.Author = model.Author;
                    existingBlogPost.ShortDescription = model.ShortDescription;
                    existingBlogPost.Content = model.Content;
                    existingBlogPost.FeaturedImageUrl = model.FeaturedImageUrl; 
                    existingBlogPost.UrlHandle  = model.UrlHandle;  
                    existingBlogPost.IsVisible = model.IsVisible;   
                    existingBlogPost.PublishedDate = model.PublishedDate;   
                     _context.BlogPosts.Update(existingBlogPost); 
                }
                else
                {
                    throw new Exception("Blog Post was not found");
                }
            }
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existBlogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existBlogPost is null)
            {
                return null;
            }
            _context.BlogPosts.Remove(existBlogPost);
            await _context.SaveChangesAsync();
            return existBlogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }
    }
}
