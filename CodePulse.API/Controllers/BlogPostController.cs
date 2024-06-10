using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        public BlogPostController(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            _blogPostRepository  = blogPostRepository;  
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var response = await _blogPostRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto model)
        {
            if(model is null)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await _blogPostRepository.CreateOrUpdateAsync(_mapper.Map<BlogPost>(model));
            var response = _mapper.Map<BlogPostDto>(blogPost);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var blogPost = await _blogPostRepository.DeleteAsync(id);
            if(blogPost is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<BlogPostDto>(blogPost);
            return Ok(response);
        }
    }
}
