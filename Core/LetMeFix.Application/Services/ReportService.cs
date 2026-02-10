using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    public class ReportService : IReportService
    {
        private readonly IGenericRepository<Reports> _generic;
        public ReportService(IGenericRepository<Reports> generic)
        { 
            _generic = generic;
        }

        public async Task AddResultToReport(string id, string reason)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.Id, id);
            var update = Builders<Reports>.Update.Set(x => x.Reason, reason).Set(x => x.UpdateDate, DateTime.Now).Set(x => x.ReportStatus, ReportStatus.Concluded);
            await _generic.UpdateWithFilter(filter, update);
        }

        public async Task<PagedResult<Reports>> GetReportsByUser(PagedRequest request, string userId)
        {
            var filter = Builders<Reports>.Filter.Where(x => x.UserId == userId || x.ReportedUserId == userId);
            return await _generic.GetPagedWithFilterAsync(request, filter);
        }
    }
}
