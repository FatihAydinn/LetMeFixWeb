using LetMeFix.Application.Interfaces;
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
    public class OfferService : IOfferService
    {
        private readonly IGenericRepository<Offers> _repository;
        public OfferService(IGenericRepository<Offers> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Offers>> GetOffersByJobIdAsync(PagedRequest request, string jobId)
        {
            var filter = Builders<Offers>.Filter.Where(x => x.JobId == jobId).ToString();
            return await _repository.FindAsync(request, filter);
        }

        public async Task<PagedResult<Offers>> GetOffersByCustomerIPerJobId(PagedRequest request, string jobId, string customerId)
        {
            var filter = Builders<Offers>.Filter.Where(x => x.JobId == jobId && x.CustomerId == customerId);
            return await _repository.FindAsync(request, filter);
        }
    }
}
