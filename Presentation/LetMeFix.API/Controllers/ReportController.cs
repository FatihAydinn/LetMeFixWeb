using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Application.Interfaces;
using LetMeFix.Application.DTOs;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        readonly private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("list-all")]
        public async Task<IActionResult> GetAllReports([FromQuery] PagedRequest request)
        {
            var filter = Builders<Reports>.Filter.Where(x => true);
            var value = await _reportService.GetPagedWithFilterAsync(request, filter);
            return Ok(value);
        }

        [HttpGet("get-byId")]
        public async Task<IActionResult> GetReportById(string id)
        {
            var value = await _reportService.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("list-byStatus")]
        public async Task<IActionResult> GetAllReportsByStatus([FromQuery] PagedRequest request, ReportStatus statusCode)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.ReportStatus, statusCode);
            var value = await _reportService.GetPagedWithFilterAsync(request, filter);
            return Ok(value);
        }

        [HttpGet("list-byUser")]
        public async Task<PagedResult<Reports>> GetReportsByUser([FromQuery] PagedRequest request, string userId)
        {
            return await _reportService.GetReportsByUser(request, userId);
        }

        [HttpPost ("create")]
        public async Task<IActionResult> CreateReport(ReportsDTO report)
        {
            var newReport = report with { Id = Guid.NewGuid().ToString() };
            await _reportService.AddAsync(newReport);
            return Ok(newReport);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReport(ReportsDTO report)
        {            
            await _reportService.UpdateAsync(report);
            return Ok(report);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _reportService.DeleteAsync(id);
            return Ok("deleted");
        }

        [HttpPut("update-reason")]
        public async Task<IActionResult> AddResultToReport(string id, string reason)
        {
            await _reportService.AddResultToReport(id, reason);
            return Ok("success");
        }
    }
}
