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
    public class LanguageService : BaseService<Languages>, ILanguageService
    {
        public LanguageService(IGenericRepository<Languages> repository) : base(repository)
        { }
    }
}
