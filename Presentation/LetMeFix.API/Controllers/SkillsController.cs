using LetMeFix.Application.Interfaces;
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
        private readonly IGenericRepository<Skills> _skillsRepository;
        private readonly ISkillsService _skillsService;
        public SkillsController(IGenericRepository<Skills> skillsRepository, ISkillsService skillsService)
        {
            _skillsRepository = skillsRepository;
            _skillsService = skillsService;
        }

        [HttpGet("getAllSkills")]
        public async Task<IActionResult> GetAllSkills([FromQuery] PagedRequest request)
        {
            var values = await _skillsRepository.GetAllAsync(request);
            return Ok(values);
        }

        [HttpPost("createSkill")]
        public async Task<IActionResult> CreateSkill([FromBody] Skills model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _skillsRepository.AddAsync(model);
            return Ok(model);
        }

        [HttpPut("updateSkill")]
        public async Task<IActionResult> UpdateSkill([FromBody] Skills skill)
        {
            await _skillsRepository.UpdateAsync(skill);
            return Ok(skill);
        }

        [HttpGet("getSkillById")]
        public async Task<IActionResult> GetSkillById(string id)
        {
            var content = await _skillsRepository.GetByIdAsync(id);
            return Ok(content);
        }

        [HttpDelete("deleteSkillById")]
        public async Task<IActionResult> DeleteSkill(string id)
        {
            await _skillsRepository.DeleteAsync(id);
            return Ok("Successfully deleted!");
        }

        [HttpGet("searchSkill")]
        public async Task<PagedResult<Skills>> SearchSkills([FromQuery] PagedRequest request, string value)
        {
            var searchValues = new List<string> { "SkillTitle" };
            return await _skillsRepository.SearchFilter(request, value, searchValues);
        }

        [HttpGet("getSkillsbyCategory")]
        public async Task<IActionResult> GetSkillsbyCategory([FromQuery] PagedRequest request, string category)
        {
            var searchValues = new List<string> { "RelatedCategories" };
            var values = await _skillsRepository.SearchFilter(request, category, searchValues);
            return Ok(values);
        }
    }
}
