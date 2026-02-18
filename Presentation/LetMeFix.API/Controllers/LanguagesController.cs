using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
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
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("get-all")]
        public async Task<PagedResult<LanguagesDTO>> GetAllLanguages([FromQuery] PagedRequest request)
        {
            return await _languageService.GetAllAsync(request);
        }

        [HttpGet("get-ById")]
        public async Task<IActionResult> GetLanguagesById(string id)
        {
            var content = await _languageService.GetByIdAsync(id);
            return Ok(content);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLanguage([FromBody] LanguagesDTO lang)
        {
            var newLang = lang with { Id = Guid.NewGuid().ToString() };
            await _languageService.AddAsync(newLang);
            return Ok(newLang);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateLanguage([FromBody] LanguagesDTO lang)
        {
            await _languageService.UpdateAsync(lang);
            return Ok(lang);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteLanguage(string id)
        {
            await _languageService.DeleteAsync(id);
            return Ok();
        }

    }
}
