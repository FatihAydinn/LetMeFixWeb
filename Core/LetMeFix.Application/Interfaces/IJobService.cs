using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IJobService
    {
        Task<List<Job>> ListJobsPerUser(string userId);
        Task<PagedResult<Job>> ListJobsPerCategory(string categoryId, PagedRequest request);
        Task<PagedResult<Job>> SearchJob(string search, PagedRequest request);
        Task<PagedResult<Job>> GetJobsPaged(FilterDefinition<Job> filter, PagedRequest request);
        Task DeleteJobWithReason(string jobId, string deleteReason);
        Task<List<Job>> FindAsync(FilterDefinition<Job> filter);
    }
}
