using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Infrastructure.Services;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categorService;
        public CategoryController(CategoryService stages)
        {
            _categorService = stages;
        }

        [HttpGet("listAllCategoryStages")]
        public async Task<IActionResult> ListAllCategoryStages()
        {
            var values = await _categorService.GetAllAsync();
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
            if (entity.PreviousParent == null) entity.FullPaths = entity.Names;
            else
            {
                var prev = await _categorService.GetPreviousCategory(entity.PreviousParent);
                entity.FullPaths = entity.Names.ToDictionary(x => x.Key, x =>
                {
                    string previousCategoryName = prev.ContainsKey(x.Key) ? prev[x.Key] : "";
                    return $"{previousCategoryName} > {x.Value}";
                });
            }
            await _categorService.AddAsync(entity);
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

        [HttpGet("searchJob")]
        public async Task<IActionResult> SearchJob(string search, [FromQuery] PagedRequest request)
        {
            var value = await _categorService.SearchCategory(search, request);
            return Ok(value);
        }

        //[HttpGet("getPreviousCategory")]
        //public async Task<IActionResult> GetPreviousCategory(string id)
        //{
        //    try
        //    {
        //        var val = await _categorService.GetPreviousCategory(id);
        //        return Ok(val);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
