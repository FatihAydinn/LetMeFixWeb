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
using ZstdSharp.Unsafe;

namespace LetMeFix.Application.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly IGenericRepository<Skills> _skillsRepository;

        public SkillsService(IGenericRepository<Skills> skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }
    }
}
