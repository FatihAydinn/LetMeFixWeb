using LetMeFix.Application.Abstraction;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryService;
        public CategoryController(IGenericRepository<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO category)
        {
            try
            {
                var content = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId,
                    Priorty = category.Priorty,
                    IsActive = category.IsActive,
                };

                await _categoryService.AddAsync(content);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
