using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly TranslationService _service;

        public TranslationController(TranslationService service)
        {
            _service = service;
        }

        [HttpGet("getTranslationsByLanguage")]
        public async Task<IActionResult> GetTranslationsByLanguage(string langId, int page, int pageSize)
        {
            var values = await _service.GetByPage(langId, page, pageSize);
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
    }
}
