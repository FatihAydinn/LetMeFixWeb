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
            await base.AddAsync(entity);
        }

        public async Task<UserInformations> GetByIdAsync(string id)
        {
            return await base.GetByIdAsync(id);
        }
        
        public async Task UpdateAsync(UserInformations entity)
        {
            await base.UpdateAsync(entity);
        }
        
        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<UserInformations>> GetAllAsync(PagedRequest request)
        {
            return await base.GetAllAsync(request);
        }

        public async Task UpdateSocials(UserInformationSocialsDTO entity)
        {
            var update = Builders<UserInformations>.Update
                .Set(x => x.LinkedIn, entity.LinkedIn)
                .Set(x => x.Instagram, entity.Instagram)
                .Set(x => x.Github, entity.Github)
                .Set(x => x.Resume, entity.Resume)
                .Set(x => x.Website, entity.Website)
                .Set(x => x.Twitter, entity.Twitter)
                .Set(x => x.UpdateDate, DateTime.Now);

            await _collection.UpdateOneAsync(x => x.Id == entity.UserId, update);
        }

        public async Task UpdateAddress(UserinformationAddressDTO entity)
        {
            var update = Builders<UserInformations>.Update
                .Set(x => x.Country, entity.Country)
                .Set(x => x.City, entity.City)
                .Set(x => x.District, entity.District)
                .Set(x => x.Neighborhood, entity.Neighborhood)
                .Set(x => x.Address, entity.Address)
                .Set(x => x.UpdateDate, DateTime.Now);
            await _collection.UpdateOneAsync(x => x.Id == entity.UserId, update);
        }

        public async Task UpdateSummary(UserInformationSummaryDTO entity)
        {
            var update = Builders<UserInformations>.Update
                .Set(x => x.AvrageRate, entity.AvrageRate)
                .Set(x => x.Reviews, entity.Reviews)
                .Set(x => x.CompletedJobs, entity.CompletedJobs)
                .Set(x => x.CompletedJobCount, entity.CompletedJobCount)
                .Set(x => x.PreferredLanguages, entity.PreferredLanguages)
                .Set(x => x.Profession, entity.Profession)
                .Set(x => x.UpdateDate, DateTime.Now);

            await _collection.UpdateOneAsync(x => x.Id == entity.UserId, update);
        }
    }
}
