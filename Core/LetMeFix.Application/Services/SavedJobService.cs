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
    public class SavedJobService : BaseService<SavedJobs>, ISavedJobService
    {
        public SavedJobService(IGenericRepository<SavedJobs> repository) : base(repository)
        { }

        public Task<PagedResult<SavedJobs>> GetSavedJobsByUserId(PagedRequest request, string userId)
        {
            var filter = Builders<SavedJobs>.Filter.Where(x => x.UserId == userId);
            var value = _repository.GetPagedWithFilterAsync(request, filter);
            return value;
        }
    }
}
