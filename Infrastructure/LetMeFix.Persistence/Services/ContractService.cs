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
    public class ContractService : BaseService<Contracts>
    {
        public ContractService(IMongoDatabase contracts) : base (contracts, "Contracts")
        {
        }

        public async Task AddAsync(Contracts entity)
        {
            await base.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<List<Contracts>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<Contracts> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Contracts entity)
        {
            await base.UpdateAsync(entity);
        }

        public async Task GiveATip(string id, decimal tip)
        {
            var filter = Builders<Contracts>.Filter.Eq(x => x.Id, id);
            var update = Builders<Contracts>.Update.Set(x => x.Tip, tip);

            await _collection.UpdateOneAsync(filter, update);
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
            var update = Builders<Contracts>.Update.Set(x => x.Status, statusval);

            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
