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
    public class SavedJobService : ISavedJobService
    {
        private readonly IGenericRepository<SavedJobs> _genericRepository;
        public SavedJobService(IGenericRepository<SavedJobs> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<PagedResult<SavedJobs>> GetSavedJobsByUserId(PagedRequest request, string userId)
        {
            var filter = Builders<SavedJobs>.Filter.Where(x => x.UserId == userId);
            var value = _genericRepository.GetPagedWithFilterAsync(request, filter);
            return value;
        }
    }
}
