using AutoMapper;
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
    public class SavedJobService : BaseService<SavedJobs, SavedJobsDTO>, ISavedJobService
    {
        public SavedJobService(IGenericRepository<SavedJobs> repository, IMapper mapper) : base(repository, mapper)
        { }

        public async Task<PagedResult<SavedJobsDTO>> GetSavedJobsByUserId(PagedRequest request, string userId)
        {
            var filter = Builders<SavedJobs>.Filter.Where(x => x.UserId == userId);
            var value = await _repository.GetPagedWithFilterAsync(request, filter);

            return new PagedResult<SavedJobsDTO>
            {
                Items = _mapper.Map<List<SavedJobsDTO>>(value.Items),
                TotalCount = value.TotalCount,
                PageSize = value.PageSize,
                Page = value.Page
            };
        }
    }
}
