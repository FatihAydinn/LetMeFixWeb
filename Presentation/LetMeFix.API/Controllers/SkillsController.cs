using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService _skillsService;
        public SkillsController(ISkillsService skillsService)
        {
            _skillsService = skillsService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSkills([FromQuery] PagedRequest request)
        {
            var values = await _skillsService.GetAllAsync(request);
            return Ok(values);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSkill([FromBody] SkillsDTO skills)
        {
            var newSkills = skills with { Id = Guid.NewGuid().ToString() };
            await _skillsService.AddAsync(newSkills);
            return Ok(newSkills);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSkill([FromBody] SkillsDTO skill)
        {
            await _skillsService.UpdateAsync(skill);
            return Ok(skill);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetSkillById(string id)
        {
            var content = await _skillsService.GetByIdAsync(id);
            return Ok(content);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            await _skillsService.DeleteAsync(id);
            return Ok("Successfully deleted!");
        }

        [HttpGet("search")]
        public async Task<PagedResult<SkillsDTO>> SearchSkills([FromQuery] PagedRequest request, string value)
        {
            var searchValues = new List<string> { "SkillTitle" };
            return await _skillsService.SearchFilter(request, value, searchValues);
        }

        [HttpGet("get-byCategory")]
        public async Task<IActionResult> GetSkillsbyCategory([FromQuery] PagedRequest request, string category)
        {
            var searchValues = new List<string> { "RelatedCategories" };
            var values = await _skillsService.SearchFilter(request, category, searchValues);
            return Ok(values);
        }
    }
}
