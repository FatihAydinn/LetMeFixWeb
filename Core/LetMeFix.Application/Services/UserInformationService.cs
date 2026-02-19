using AutoMapper;
using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
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

namespace LetMeFix.Application.Services
{
    public class UserInformationService : BaseService<UserInformations, UserInformationsDTO>, IUserInformationService
    {
        public UserInformationService(IGenericRepository<UserInformations> repository, IMapper mapper) : base(repository, mapper)
        { }

        public async Task UpdateSocials(UserInformationSocialsDTO entity)
        {
            var filter = Builders<UserInformations>.Filter.Where(x => x.Id == entity.UserId);
            var update = Builders<UserInformations>.Update
                .Set(x => x.LinkedIn, entity.LinkedIn)
                .Set(x => x.Instagram, entity.Instagram)
                .Set(x => x.Github, entity.Github)
                .Set(x => x.Resume, entity.Resume)
                .Set(x => x.Website, entity.Website)
                .Set(x => x.Twitter, entity.Twitter)
                .Set(x => x.UpdateDate, DateTime.Now);

            await _repository.UpdateWithFilter(filter, update);
        }

        public async Task UpdateAddress(UserinformationAddressDTO entity)
        {
            var filter = Builders<UserInformations>.Filter.Where(x => x.Id == entity.UserId);
            var update = Builders<UserInformations>.Update
                .Set(x => x.Country, entity.Country)
                .Set(x => x.City, entity.City)
                .Set(x => x.District, entity.District)
                .Set(x => x.Neighborhood, entity.Neighborhood)
                .Set(x => x.Address, entity.Address)
                .Set(x => x.UpdateDate, DateTime.Now);
            await _repository.UpdateWithFilter(filter, update);
        }

        public async Task UpdateSummary(UserInformationSummaryDTO entity)
        {
            var filter = Builders<UserInformations>.Filter.Where(x => x.Id == entity.UserId);
            var update = Builders<UserInformations>.Update
                .Set(x => x.AvrageRate, entity.AvrageRate)
                .Set(x => x.Reviews, entity.Reviews)
                .Set(x => x.CompletedJobs, entity.CompletedJobs)
                .Set(x => x.CompletedJobCount, entity.CompletedJobCount)
                .Set(x => x.PreferredLanguages, entity.PreferredLanguages)
                .Set(x => x.Profession, entity.Profession)
                .Set(x => x.UpdateDate, DateTime.Now);

            await _repository.UpdateWithFilter(filter, update);
        }
    }
}
