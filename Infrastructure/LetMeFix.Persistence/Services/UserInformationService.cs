using LetMeFix.Application.DTOs;
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
    public class UserInformationService : BaseService<UserInformations>
    {
        //private readonly IMongoCollection<UserInformations> _collection;
        //public UserInformationService(IMongoDatabase database)
        //{
        //    _collection = database.GetCollection<UserInformations>("UserInformations");
        //}

        public UserInformationService(IMongoDatabase database) : base (database, "UserInformations")
        {
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

        public async Task UpdateSocials(UserInformationSocialsDTO entity)
        {
            var update = Builders<UserInformations>.Update
                .Set(x => x.LinkedIn, entity.LinkedIn)
                .Set(x => x.Instagram, entity.Instagram)
                .Set(x => x.Github, entity.Github)
                .Set(x => x.Resume, entity.Resume)
                .Set(x => x.Website, entity.Website)
                .Set(x => x.Twitter, entity.Twitter);

            await _collection.UpdateOneAsync(x => x.Id == entity.UserId, update);
        }

        public async Task UpdateAddress(UserinformationAddressDTO entity)
        {
            var update = Builders<UserInformations>.Update
                .Set(x => x.Country, entity.Country)
                .Set(x => x.City, entity.City)
                .Set(x => x.District, entity.District)
                .Set(x => x.Neighborhood, entity.Neighborhood)
                .Set(x => x.Address, entity.Address);
            await _collection.UpdateOneAsync(x => x.Id == entity.userId, update);
        }
    }
}
