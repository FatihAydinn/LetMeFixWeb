using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
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
            await _collection.InsertOneAsync(entity);
        }

        //customer
        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        //admin
        public async Task<List<Review>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Review entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
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
    }
}
