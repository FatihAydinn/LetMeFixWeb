using AutoMapper;
using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    public class ReviewService : BaseService<Review, ReviewDTO>, IReviewService
    {
        public ReviewService(IGenericRepository<Review> repository, IMapper mapper) : base(repository, mapper)
        { }

        public async Task<PagedResult<Review>> GetReviewsByJobId(PagedRequest request, string id)
        {
            var filter = Builders<Review>.Filter.Where(x => x.JobId == id);
            return await _repository.GetPagedWithFilterAsync(request, filter);
        }

        public async Task<PagedResult<Review>> GetReviewsByUserId(PagedRequest request, string id)
        {
            var filter = Builders<Review>.Filter.Where(x => x.CustomerId == id);
            return await _repository.GetPagedWithFilterAsync(request, filter);
        }
    }
}
