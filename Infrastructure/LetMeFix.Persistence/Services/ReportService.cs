using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
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
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<Reports>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Reports> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Reports entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task<List<Reports>> GetReportsByStatusAsync(int statuscode)
        {
            var status = (ReportStatus)statuscode;
            
            return await _collection.Find(x => x.ReportStatus == status).ToListAsync();
        }

        public async Task<List<Reports>> GetReportsByUser(string userId)
        {
            return await _collection.Find(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Reports>> GetReportsByReportedUser(string userId)
        {
            return await _collection.Find(x => x.ReportedUserId == userId).ToListAsync();
        }

        public async Task AddResultToReport(Reports result)
        {
            await _collection.ReplaceOneAsync(x => x.Id == result.Id, result);
        }
    }
}
