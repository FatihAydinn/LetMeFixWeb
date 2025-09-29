using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IGenericRepository<Skills> _skills;
        public SkillsController(IGenericRepository<Skills> skills)
        {
            _skills = skills;
        }

        [HttpPost("createSkill")]
        public async Task<IActionResult> CreateSkill([FromBody] Skills model)
        {
            try
            {
                model.SkillId = Guid.NewGuid().ToString();
                await _skills.AddAsync(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            try
            {
                var values = await _skills.GetAllAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut("updateSkill")]
        public async Task<IActionResult> UpdateSkill([FromBody] Skills skill)
        {
            try
            {
                await _skills.UpdateAsync(skill);
                return Ok(skill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getSkillById")]
        public async Task<IActionResult> GetSkillById(string id)
        {
            try
            {
                var content = await _skills.GetByIdAsync(id);
                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteSkillById")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            try
            {
                await _skills.DeleteAsync(id);
                return Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
