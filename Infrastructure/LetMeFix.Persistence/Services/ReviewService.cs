using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    //public class ReviewService : IGenericRepository<Review>
    public class ReviewService : BaseService<Review>
    {
        public ReviewService(IMongoDatabase database) : base (database, "Review") { }

        public async Task AddAsync(Review entity)
        {
            await base.AddAsync(entity);
        }

        //customer
        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        //admin
        public async Task<PagedResult<Review>> GetAllAsync(PagedRequest request)
        {
            return await base.GetAllAsync(request);
        }

        public async Task<Review> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Review entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task<List<Review>> GetReviewsByProvideId(string id)
        {
            return await _collection.Find(x => x.ProviderId == id).ToListAsync();
        }

        public async Task<List<Review>> GetReviewsByJobId(string id)
        {
            return await _collection.Find(x => x.JobId == id).ToListAsync();
        }

        //admin
        public async Task<List<Review>> GetReviewsByCustomerId(string id)
        {
            return await _collection.Find(x => x.CustomerId == id).ToListAsync();
        }

        public async Task<PagedResult<Review>> GetJobReviewsPaged(FilterDefinition<Review> filter, PagedRequest request)
        {
            return await GetPagedWithFilterAsync(request, filter);
        }
    }
}
