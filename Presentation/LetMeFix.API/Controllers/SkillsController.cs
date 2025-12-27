using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly SkillsService _skills;
        public SkillsController(SkillsService skills)
        {
            _skills = skills;
        }

        [HttpGet("getAllSkills")]
        public async Task<IActionResult> GetAllSkills([FromQuery] PagedRequest request)
        {
            var filter = Builders<Skills>.Filter.Where(x => true);
            var values = await _skills.GetPaged(request, filter);
            return Ok(values);
        }

        [HttpPost("createSkill")]
        public async Task<IActionResult> CreateSkill([FromBody] Skills model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _skills.AddAsync(model);
            return Ok(model);
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

        [HttpGet("getSkillsbyCategory")]
        public async Task<IActionResult> GetSkillsbyCategory([FromQuery] PagedRequest request, string category)
        {
            var filter = Builders<Skills>.Filter.Where(x => x.RelatedCategories.Contains(category));
            var values = await _skills.GetPaged(request, filter);
            return Ok(values);
        }

        [HttpGet("searchSkill")]
        public async Task<PagedResult<Skills>> SearchSkills([FromQuery] PagedRequest request, string value)
        {
            return await _skills.SearchSkill(request, value);
        }
    }
}
