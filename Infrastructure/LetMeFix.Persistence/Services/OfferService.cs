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
    //public class OfferService : IOfferRepository
    public class OfferService : BaseService<Offers>
    {
        //private readonly IMongoCollection<Offers> _collection;
        //public OfferService(IMongoDatabase database)
        //{
        //    _collection = database.GetCollection<Offers>("Offers");
        //}

        public OfferService(IMongoDatabase database) : base (database, "Offers") { }

        public async Task<List<Offers>> GetOffersByJobIdAsync(string jobId)
        {
            return await _collection.Find(x => x.JobId == jobId).ToListAsync();
        }

        public async Task<List<Offers>> GetOffersByCustomerIPerJobId(string jobId, string customerId)
        {
            var value = await _collection.Find(x => x.JobId == jobId && x.CustomerId == customerId).ToListAsync();
            return value;
        }
    }
}
