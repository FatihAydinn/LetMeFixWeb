using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IJobService : IBaseService<Job, JobDTO>
    {
        Task<PagedResult<Job>> GetAllAsync(PagedRequest request);
        Task<PagedResult<Job>> ListJobsPerUser(string userId, PagedRequest request);
        Task<PagedResult<Job>> ListJobsPerCategory(string categoryId, PagedRequest request);
        Task<PagedResult<Job>> SearchJob(string search, PagedRequest request);
        Task<PagedResult<Job>> GetJobsPaged(FilterDefinition<Job> filter, PagedRequest request);
        Task DeleteJobWithReason(string jobId, string deleteReason);
    }
}
