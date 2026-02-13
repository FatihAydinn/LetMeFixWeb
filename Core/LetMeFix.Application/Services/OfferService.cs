using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
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

        public async Task<bool> CreateOfferAsync(Offers offer)
        {
            var job = await _jobGenericRepository.GetByIdAsync(offer.JobId);

            if (job == null || !job.IsActive)
                throw new Exception("Bu iş artık aktif değil");

            if (job.Version != offer.JobVersion)
                throw new Exception("İş durumu değişmiş. Sayfayı yenileyin.");

            await _repository.AddAsync(offer);
            return true;

        }
    }
}
