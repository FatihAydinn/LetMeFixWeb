using LetMeFix.Domain.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Application.DTOs;
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

        [HttpGet("getById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var content = await _categoryService.GetByIdAsync(id);
                return Ok(content);
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
                    Id = Guid.NewGuid().ToString(),
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

        [HttpPost("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category)
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
                await _categoryService.UpdateAsync(content);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteCategory")]
        public async Task DeleteCategory(string id)
        {
            await _categoryService.DeleteAsync(id);
        }
    }
}
