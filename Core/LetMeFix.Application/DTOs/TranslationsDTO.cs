using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record TranslationsDTO(
        string? Id,
        DateTime? CreateDate,
        DateTime? UpdateDate,
        string Key,
        string LanguageId,
        string Content
    );
}
