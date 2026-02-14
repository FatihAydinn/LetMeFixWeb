using LetMeFix.Application.Services;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetMeFix.Domain.Interfaces;
using static MongoDB.Driver.WriteConcern;
using SharpCompress.Common;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using LetMeFix.Application.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LetMeFix.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IGenericRepository<Job> _repository;
        private readonly IGenericRepository<Category> _categoryService;
        ContractService _contractStages;
        JobService _job;

        public JobService(IGenericRepository<Job> repository, IGenericRepository<Category> categoryService)
        {
            _repository = repository;
            _categoryService = categoryService;
        }

        private async Task<string> GetCategoryPaths(Job item)
        {
            var categoryfullpath = await _categoryService.GetByIdAsync(item.CategoryId.Substring(item.CategoryId.Length - 3));
            return categoryfullpath.FullPaths["EN"];
        }

        public async Task<PagedResult<Job>> GetAllAsync(PagedRequest request)
        {
            var values = await _repository.GetAllAsync(request);
            foreach (var item in values.Items)
            {
                var fullpath = await GetCategoryPaths(item);
                item.CategoryPath = fullpath;
            }
            return values; 
        }

        public async Task<PagedResult<Job>> ListJobsPerUser(PagedRequest request, string userId)
        {
            var filter = Builders<Job>.Filter.Eq(x => x.ProviderId, userId);
            return await _repository.FindAsync(request, filter);
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
            return await _repository.SearchFilter(request, search, fields);
        }

        public async Task<PagedResult<Job>> GetJobsPaged(FilterDefinition<Job> filter, PagedRequest request)
        {
            return await _repository.GetPagedWithFilterAsync(request, filter);
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

        public Task<PagedResult<Job>> FindAsync(FilterDefinition<Job> filter)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Job>> ListJobsPerUser(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
