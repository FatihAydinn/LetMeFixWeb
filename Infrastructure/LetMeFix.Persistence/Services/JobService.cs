using LetMeFix.Infrastructure.Services;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Services;
using static MongoDB.Driver.WriteConcern;
using SharpCompress.Common;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Application.Interfaces;

namespace LetMeFix.Infrastructure.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _repository;
        ContractService _contractStages;

        public JobService(IGenericRepository<Job> repository) 
        {
            _repository = repository;
        }

        private async Task GetCategoryPaths(Job item)
        {
            if (item != null)
            {
                var categoryfullpath = await _repository.GetByIdAsync(item.CategoryId.Substring(item.CategoryId.Length - 3));
                item.CategoryPath = categoryfullpath.CategoryPath;
            }
        }

        public async Task<List<Job>> GetAllAsync()
        {
            var values = await _repository.GetAllAsync();
            foreach (var item in values)
            {
                await GetCategoryPaths(item);
            }
            return values; 
        }

        public async Task<List<Job>> ListJobsPerUser(string userId)
        {
            var filter = Builders<Job>.Filter.Eq(x => x.ProviderId, userId);
            return await _repository.FindAsync(filter);
        } 

        public async Task<PagedResult<Job>> ListJobsPerCategory(string categoryId, PagedRequest request)
        {
            var filter = Builders<Job>.Filter.Where(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId));
            //var value = await _collection.Find(x => x.CategoryId.Length % 3 == 0 && x.CategoryId.Contains(categoryId)).ToListAsync();
            return await GetJobsPaged(filter, request);
        }

        public async Task<PagedResult<Job>> SearchJob(string search, PagedRequest request)
        {
            var fields = new List<string> { "Title" };
            return await _repository.SearchFilter(search, fields, request);
        }

        public async Task<PagedResult<Job>> GetJobsPaged(FilterDefinition<Job> filter, PagedRequest request)
        {
            return await _repository.GetPagedWithFilterAsync(filter, request);
        }

        public async Task DeleteJobWithReason(string jobId, string deleteReason)
        {
            var job = await _repository.GetByIdAsync(jobId);
            if (job == null) return;
            job.DeleteReason = deleteReason;
            //var filter = Builders<Job>.Filter.Eq(x => x.Id, jobId);
            //var update = Builders<Job>.Update.Set(x => x.DeleteReason, deleteReason);

            await _repository.UpdateAsync(job);
            await _contractStages.ChangeStatus(jobId, 4);
        }

        public Task<List<Job>> FindAsync(FilterDefinition<Job> filter)
        {
            throw new NotImplementedException();
        }
    }
}
