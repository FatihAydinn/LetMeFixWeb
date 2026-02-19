using AutoMapper;
using Humanizer;
using LetMeFix.Application.DTOs;
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
    public class OfferService : BaseService<Offers, OffersDTO>, IOfferService
    {
        private readonly IGenericRepository<Job> _jobGenericRepository;
        public OfferService(IGenericRepository<Offers> repository, IGenericRepository<Job> jobGenericRepository, IMapper mapper) : base(repository, mapper)
        {
            _jobGenericRepository = jobGenericRepository;
        }

        public async Task<PagedResult<OffersDTO>> GetOffersByJobIdAsync(PagedRequest request, string jobId)
        {
            var filter = Builders<Offers>.Filter.Where(x => x.JobId == jobId).ToString();
            var result = await _repository.GetPagedWithFilterAsync(request, filter);

            return new PagedResult<OffersDTO>
            {
                Items = _mapper.Map<List<OffersDTO>>(result.Items),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }

        public async Task<PagedResult<OffersDTO>> GetOffersByCustomerIPerJobId(PagedRequest request, string jobId, string customerId)
        {
            var filter = Builders<Offers>.Filter.Where(x => x.JobId == jobId && x.CustomerId == customerId);
            var result = await _repository.GetPagedWithFilterAsync(request, filter);

            return new PagedResult<OffersDTO>
            {
                Items = _mapper.Map<List<OffersDTO>>(result.Items),
                TotalCount = result.TotalCount,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }

        public async Task<bool> CreateOfferAsync(OffersDTO offer)
        {
            var job = await _jobGenericRepository.GetByIdAsync(offer.JobId);
            var entity = _mapper.Map<Offers>(offer);

            if (job == null || !job.IsActive)
                throw new Exception("Bu iş artık aktif değil");

            if (job.Version != offer.JobVersion)
                throw new Exception("İş durumu değişmiş. Sayfayı yenileyin.");

            await _repository.AddAsync(entity);
            return true;

        }
    }
}
