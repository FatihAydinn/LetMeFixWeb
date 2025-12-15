using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LetMeFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        readonly private ReportService _service;
        public ReportController(ReportService service)
        {
            _service = service;
        }

        [HttpGet("listAllReports")]
        public async Task<IActionResult> GetAllReports(int page, int pageSize)
        {
            var filter = Builders<Reports>.Filter.Where(x => true);
            var value = await _service.GetPagedWithFilterAsync(filter, page, pageSize);
            return Ok(value);
        }

        [HttpGet("getReportbyId")]
        public async Task<IActionResult> GetReportById(string id)
        {
            var value = await _service.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("listReportsByStatus")]
        public async Task<IActionResult> GetAllReportsByStatus(ReportStatus statusCode, int page, int pageSize)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.ReportStatus, statusCode);
            var value = await _service.GetPagedWithFilterAsync(filter, page, pageSize);
            return Ok(value);
        }

        [HttpGet("listReportsByUser")]
        public async Task<IActionResult> GetReportsByUser(string userId, int page, int pageSize)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.UserId, userId);
            var value = await _service.GetPagedWithFilterAsync(filter, page, pageSize);
            return Ok(value);
        }

        [HttpGet("listReportsByReportedUser")]
        public async Task<IActionResult> ListReportsByReportedUser(string userId, int page, int pageSize)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.ReportedUserId, userId);
            var value = await _service.GetPagedWithFilterAsync(filter, page, pageSize);
            return Ok(value);
        }

        [HttpPost ("createReport")]
        public async Task<IActionResult> CreateReport(Reports report)
        {
            report.Id = Guid.NewGuid().ToString();
            await _service.AddAsync(report);
            return Ok(report);
        }

        [HttpPut("updateReport")]
        public async Task<IActionResult> UpdateReport(Reports report)
        {            
            await _service.UpdateAsync(report);
            return Ok(report);
        }

        [HttpDelete("deleteReport")]
        public async Task<IActionResult> DeleteReport(string id)
        {
            await _service.DeleteAsync(id);
            return Ok("deleted");
        }

        [HttpPut("updateReson")]
        public async Task<IActionResult> AddResultToReport(Reports reports)
        {
            reports.ReportStatus = ReportStatus.Concluded;
            await _service.AddResultToReport(reports);
            return Ok(reports);
        }
    }
}
