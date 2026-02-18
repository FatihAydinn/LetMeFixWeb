using LetMeFix.Application.DTOs;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface ITranslationService : IBaseService<Translations, TranslationsDTO>
    {
        Task<PagedResult<TranslationsDTO>> SearchFilter(PagedRequest request, string search, List<string> fieldNames);
    }
}
