using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
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
        private readonly CategoryService _stages;
        public CategoryController(CategoryService stages)
        {
            _stages = stages;
        }

        [HttpGet("listAllCategoryStages")]
        public async Task<IActionResult> ListAllCategoryStages()
        {
            var values = await _stages.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("getCategoryStagebyId")]
        public async Task<IActionResult> GetCategoryStagebyId(string id)
        {
            var value = await _stages.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("createCategoryStage")]
        public async Task<IActionResult> CreateCategoryStage([FromBody] Category entity)
        { //TR, EN
            if (entity.PreviousParent == null) entity.FullPaths = entity.Names;
            else
            {
                var prev = await _stages.GetPreviousCategory(entity.PreviousParent);
                entity.FullPaths = entity.Names.ToDictionary(x => x.Key, x =>
                {
                    string previousCategoryName = prev.ContainsKey(x.Key) ? prev[x.Key] : "";
                    return $"{previousCategoryName} > {x.Value}";
                });
            }
            await _stages.AddAsync(entity);
            return Ok(entity);
        }

        [HttpPut("updateCategoryStage")]
        public async Task<IActionResult> UpdateCategoryStage([FromBody] Category entity)
        {
            await _stages.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("deleteCategoryStage")]
        public async Task<IActionResult> DeleteCategoryStage(string id)
        {
            await _stages.DeleteAsync(id);
            return Ok("success");
        }

        //[HttpGet("getPreviousCategory")]
        //public async Task<IActionResult> GetPreviousCategory(string id)
        //{
        //    try
        //    {
        //        var val = await _stages.GetPreviousCategory(id);
        //        return Ok(val);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
