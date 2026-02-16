using AutoMapper;
using Humanizer;
using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Globalization;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categorService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categorService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("list-all")]
        public async Task<IActionResult> ListAllCategoryStages([FromQuery] PagedRequest request)
        {
            var filter = Builders<Category>.Filter.Where(x => true);
            var values = await _categorService.GetPagedWithFilterAsync(request, filter);

            return Ok(values);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetCategoryStagebyId(string id)
        {
            var value = await _categorService.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryStage([FromBody] CategoryDTO dto)
        { //TR, EN
            await _categorService.CreateCategoryStage(dto);
            return Ok(dto);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategoryStage([FromBody] CategoryDTO dto)
        {
            await _categorService.UpdateAsync(dto);
            return Ok(dto);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategoryStage(string id)
        {
            await _categorService.DeleteAsync(id);
            return Ok("success");
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCategory(string search, [FromQuery] PagedRequest request)
        {
            var value = await _categorService.SearchCategory(search, request);
            return Ok(value);
        }
    }
}
