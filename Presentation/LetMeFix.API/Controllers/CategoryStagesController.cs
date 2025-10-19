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
    public class CategoryStagesController : ControllerBase
    {
        private readonly CategoryStageService _stages;
        public CategoryStagesController(CategoryStageService stages)
        {
            _stages = stages;
        }

        [HttpGet("listAllCategoryStages")]
        public async Task<IActionResult> ListAllCategoryStages()
        {
            try
            {
                var values = await _stages.GetAllAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCategoryStagebyId")]
        public async Task<IActionResult> GetCategoryStagebyId(string id)
        {
            try
            {
                var value = await _stages.GetByIdAsync(id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createCategoryStage")]
        public async Task<IActionResult> CreateCategoryStage([FromBody] CategoryStages entity)
        {
            try
            {

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateCategoryStage")]
        public async Task<IActionResult> UpdateCategoryStage([FromBody] CategoryStages entity)
        {
            try
            {
                await _stages.UpdateAsync(entity);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteCategoryStage")]
        public async Task<IActionResult> DeleteCategoryStage(string id)
        {
            try
            {
                await _stages.DeleteAsync(id);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
