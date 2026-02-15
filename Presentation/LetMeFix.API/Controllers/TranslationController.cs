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
    public class TranslationController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        public TranslationController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet("getTranslationsByLanguage")]
        public async Task<IActionResult> GetTranslationsByLanguage([FromQuery] PagedRequest request, string langId)
        {
            langId = langId.ToLower();
            var filter = Builders<Translations>.Filter.Where(x => x.LanguageId == langId);
            var values = await _translationService.GetPagedWithFilterAsync(request, filter);
            return Ok(values);
        }

        [HttpPost("createTrasnlation")]
        public async Task<IActionResult> CreateTranslation([FromBody] Translations translations)
        {
            translations.Id = Guid.NewGuid().ToString();
            await _translationService.AddAsync(translations);
            return Ok(translations);
        }

        [HttpPut("updateTranslation")]
        public async Task<IActionResult> UpdateTranslation([FromBody] Translations translations)
        {
            await _translationService.UpdateAsync(translations);
            return Ok(translations);
        }

        [HttpDelete("deleteTranslation")]
        public async Task<IActionResult> DeleteTranslation(string id)
        {
            await _translationService.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("searchTranslation")]
        public async Task<IActionResult> SearchTranslation([FromQuery] PagedRequest request, string search)
        {
            var fieldName = new List<string> { "Key", "Content" };
            var value = await _translationService.SearchFilter(request, search, fieldName);
            return Ok(value);
        }
    }
}
