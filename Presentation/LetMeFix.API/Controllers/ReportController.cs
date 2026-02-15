using LetMeFix.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Application.Interfaces;

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

        [HttpGet("listAllReports")]
        public async Task<IActionResult> GetAllReports([FromQuery] PagedRequest request)
        {
            var filter = Builders<Reports>.Filter.Where(x => true);
            var value = await _reportService.GetPagedWithFilterAsync(request, filter);
            return Ok(value);
        }

        [HttpGet("getReportbyId")]
        public async Task<IActionResult> GetReportById(string id)
        {
            var value = await _reportService.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("listReportsByStatus")]
        public async Task<IActionResult> GetAllReportsByStatus([FromQuery] PagedRequest request, ReportStatus statusCode)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.ReportStatus, statusCode);
            var value = await _reportService.GetPagedWithFilterAsync(request, filter);
            return Ok(value);
        }

        [HttpGet("listReportsByUser")]
        public async Task<PagedResult<Reports>> GetReportsByUser([FromQuery] PagedRequest request, string userId)
        {
            return await _reportService.GetReportsByUser(request, userId);
        }

        [HttpPost ("createReport")]
        public async Task<IActionResult> CreateReport(Reports report)
        {
            report.Id = Guid.NewGuid().ToString();
            await _reportService.AddAsync(report);
            return Ok(report);
        }

        [HttpPut("updateReport")]
        public async Task<IActionResult> UpdateReport(Reports report)
        {            
            await _reportService.UpdateAsync(report);
            return Ok(report);
        }

        [HttpDelete("deleteReport")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _reportService.DeleteAsync(id);
            return Ok("deleted");
        }

        [HttpPut("updateReson")]
        public async Task<IActionResult> AddResultToReport(string id, string reason)
        {
            await _reportService.AddResultToReport(id, reason);
            return Ok("success");
        }
    }
}
