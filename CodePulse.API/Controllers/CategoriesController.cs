using AutoMapper;
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTOs;
using CodePulse.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto model)
        {
            if(model is null)
            {
                return BadRequest();
            }
            var response =  await _categoryRepository.CreateOrUpdateAsync(_mapper.Map<Category>(model));    
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories =await _categoryRepository.GetAllAsync();
            var response = _mapper.Map<List<CategoryDto>>(categories);  
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var existCategory = await _categoryRepository.GetCategoryById(id);
            if(existCategory is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<CategoryDto>(existCategory);
            return Ok(response);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if(category is null)
            {
                return NotFound();
            }
            var response = _mapper.Map<CategoryDto>(category);
            return Ok(response);    
        }
    }
}
