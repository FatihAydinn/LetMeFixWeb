using AutoMapper;
using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    public class LanguageService : BaseService<Languages, LanguagesDTO>, ILanguageService
    {
        public LanguageService(IGenericRepository<Languages> repository, IMapper mapper) : base(repository, mapper)
        { }
    }
}
