using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IGenericRepository<Languages> _languageService;

        public LanguagesController(IGenericRepository<Languages> languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("getAllLanguages")]
        public async Task<IActionResult> GetAllLanguages()
        {
            var content = await _languageService.GetAllAsync();
            return Ok(content);
        }

        [HttpGet("getLanguagesById")]
        public async Task<IActionResult> GetLanguagesById(string id)
        {
            var content = await _languageService.GetByIdAsync(id);
            return Ok(content);
        }

        [HttpPost("createLanguage")]
        public async Task<IActionResult> CreateLanguage([FromBody] Languages lang)
        {
            lang.Id = Guid.NewGuid().ToString();
            await _languageService.AddAsync(lang);
            return Ok(lang);
        }

        [HttpPut("updateLanguage")]
        public async Task<IActionResult> UpdateLanguage([FromBody] Languages lang)
        {
            await _languageService.UpdateAsync(lang);
            return Ok(lang);
        }

        [HttpDelete("deleteLanguage")]
        public async Task<IActionResult> DeleteLanguage(string id)
        {
            await _languageService.DeleteAsync(id);
            return Ok();
        }

    }
}
