using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Globalization;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorService;

        public CategoryController(ICategoryService categoryService)
        {
            _categorService = categoryService;
        }

        [HttpGet("listAllCategoryStages")]
        public async Task<IActionResult> ListAllCategoryStages([FromQuery] PagedRequest request)
        {
            var filter = Builders<Category>.Filter.Where(x => true);
            var values = await _categorService.GetPagedWithFilterAsync(request, filter);
            return Ok(values);
        }

        [HttpGet("getCategoryStagebyId")]
        public async Task<IActionResult> GetCategoryStagebyId(string id)
        {
            var value = await _categorService.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("createCategoryStage")]
        public async Task<IActionResult> CreateCategoryStage([FromBody] Category entity)
        { //TR, EN
            await _categorService.CreateCategoryStage(entity);
            return Ok(entity);
        }

        [HttpPut("updateCategoryStage")]
        public async Task<IActionResult> UpdateCategoryStage([FromBody] Category entity)
        {
            await _categorService.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("deleteCategoryStage")]
        public async Task<IActionResult> DeleteCategoryStage(string id)
        {
            await _categorService.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("searchCategory")]
        public async Task<IActionResult> SearchCategory(string search, [FromQuery] PagedRequest request)
        {
            var value = await _categorService.SearchCategory(search, request);
            return Ok(value);
        }
    }
}
