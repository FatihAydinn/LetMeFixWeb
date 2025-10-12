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
        private readonly IGenericRepository<CategoryStages> _stages;
        public CategoryStagesController(IGenericRepository<CategoryStages> stages)
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
    }
}
