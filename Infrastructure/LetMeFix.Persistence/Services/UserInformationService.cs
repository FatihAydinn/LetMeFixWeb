using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using Microsoft.Identity.Client;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    public class UserInformationService : IGenericRepository<UserInformations>
    {
        private readonly IMongoCollection<UserInformations> _collection;

        public UserInformationService(IMongoDatabase database)
        {
            _collection = database.GetCollection<UserInformations>("UserInformations");
        }

        public async Task AddAsync(UserInformations entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<UserInformations> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task UpdateAsync(UserInformations entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
        
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserInformations>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
