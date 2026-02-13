using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    public class ContractService : IContractService
    {
        private readonly IGenericRepository<Contracts> _repository;
        public ContractService(IGenericRepository<Contracts> repository)
        {
            _repository = repository;
        }

        public async Task GiveATip(string id, decimal tip)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.Id, id);
            var update = Builders<Contracts>.Update.Set(x => x.Tip, tip);

            await _repository.UpdateWithFilter(filter, update);
        }

        public async Task ChangeStatus(string id, int status)
        {
            var statusval = JobStatus.Waiting;
            switch (status)
            {
                case 1:
                    statusval = JobStatus.Waiting;
                    break;
                case 2:
                    statusval = JobStatus.InProgress;
                    break;
                case 3:
                    statusval = JobStatus.Completed;
                    break;
                case 4:
                    statusval = JobStatus.Cancelled;
                    break;
                case 5:
                    statusval = JobStatus.Expired;
                    break;
            }
            var filter = Builders<Contracts>.Filter.Eq(x => x.Id, id);
            var update = Builders<Contracts>.Update.Set(x => x.Status, statusval).Set(x => x.UpdateDate, DateTime.Now);

            await _repository.UpdateWithFilter(filter, update);
        }

        public Task<PagedResult<Contracts>> GetContractsByProviderId(PagedRequest request, string userId)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.ProviderId, userId);
            return _repository.FindAsync(request, filter);
        }

        public Task<PagedResult<Contracts>> GetContractsByClientId(PagedRequest request, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Contracts>> GetContractByStatusAndUserId(PagedRequest request, string userId, int status)
        {
            var enumStatus = (JobStatus)status;
            var userfilter = Builders<Contracts>.Filter.Or(
                         Builders<Contracts>.Filter.Eq(x => x.ProviderId, userId),
                         Builders<Contracts>.Filter.Eq(x => x.ClientId, userId)
            );
            var filter = Builders<Contracts>.Filter.And(
                         userfilter,
                         Builders<Contracts>.Filter.Eq(x => x.Status, enumStatus)
            );
            return await _repository.GetPagedWithFilterAsync(request, filter);
        }
    }
}
