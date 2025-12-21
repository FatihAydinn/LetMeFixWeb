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
    public class ReportService : BaseService<Reports>
    {
        public ReportService(IMongoDatabase database) : base (database, "Reports")
        { 
        }

        public async Task AddAsync(Reports entity)
        {
            await base.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Reports>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Reports> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Reports entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task AddResultToReport(string id, string reason)
        {
            var filter = Builders<Reports>.Filter.Eq(x => x.Id, id);
            var update = Builders<Reports>.Update.Set(x => x.Reason, reason).Set(x => x.UpdateDate, DateTime.Now).Set(x => x.ReportStatus, ReportStatus.Concluded);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<PagedResult<Reports>> GetJobReviewsPaged(FilterDefinition<Reports> filter, PagedRequest request)
        {
            return await GetPagedWithFilterAsync(filter, request);
        }
    }
}
