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
            model.SkillId = Guid.NewGuid().ToString();
            await _skills.AddAsync(model);
            return Ok(model);
        }

        [HttpGet("getAllSkills")]
        public async Task<IActionResult> GetAllSkills()
        {
            var values = await _skills.GetAllAsync();
            return Ok(values);
        }

        [HttpPut("updateSkill")]
        public async Task<IActionResult> UpdateSkill([FromBody] Skills skill)
        {
            await _skills.UpdateAsync(skill);
            return Ok(skill);
        }

        [HttpGet("getSkillById")]
        public async Task<IActionResult> GetSkillById(string id)
        {
            var content = await _skills.GetByIdAsync(id);
            return Ok(content);
        }

        [HttpDelete("deleteSkillById")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            await _skills.DeleteAsync(id);
            return Ok("Successfully deleted!");
        }
    }
}
