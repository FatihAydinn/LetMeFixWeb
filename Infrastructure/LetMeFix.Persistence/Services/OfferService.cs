using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using LetMeFix.Persistence.Repository;
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
        private readonly IGenericRepository<Job> _jobGenericRepository;
        public OfferService(IGenericRepository<Offers> repository, IGenericRepository<Job> jobGenericRepository)
        {
            _repository = repository;
            _jobGenericRepository = jobGenericRepository;
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

        public async Task<bool> CreateOfferAsync(string jobId, string customerId, decimal price)
        {
            var job = await _jobGenericRepository.GetByIdAsync(jobId);

            if (job == null || !job.IsActive)
                throw new Exception("Bu iş artık aktif değil");

            var offer = new Offers
            {
                JobId = jobId,
                CustomerId = customerId,
                Price = price,
                JobVersion = job.Version
            };

            await _repository.AddAsync(offer);
            return true;

        }
    }
}
