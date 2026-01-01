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
    public class TranslationController : ControllerBase
    {
        private readonly IGenericRepository<Translations> _service;

        public TranslationController(IGenericRepository<Translations> service)
        {
            _service = service;
        }

        [HttpGet("getTranslationsByLanguage")]
        public async Task<IActionResult> GetTranslationsByLanguage([FromQuery] PagedRequest request, string langId)
        {
            langId = langId.ToLower();
            var filter = Builders<Translations>.Filter.Where(x => x.LanguageId == langId);
            var values = await _service.GetPagedWithFilterAsync(request, filter);
            return Ok(values);
        }

        [HttpPost("createTrasnlation")]
        public async Task<IActionResult> CreateTranslation([FromBody] Translations translations)
        {
            translations.Id = Guid.NewGuid().ToString();
            await _service.AddAsync(translations);
            return Ok(translations);
        }

        [HttpPut("updateTranslation")]
        public async Task<IActionResult> UpdateTranslation([FromBody] Translations translations)
        {
            await _service.UpdateAsync(translations);
            return Ok(translations);
        }

        [HttpDelete("deleteTranslation")]
        public async Task<IActionResult> DeleteTranslation(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("searchTranslation")]
        public async Task<IActionResult> SearchTranslation([FromQuery] PagedRequest request, string search)
        {
            var fieldName = new List<string> { "Key", "Content" };
            var value = await _service.SearchFilter(request, search, fieldName);
            return Ok(value);
        }
    }
}
