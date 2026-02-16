using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record CategoryDTO(
            string Id,
            Dictionary<string, string> Names,
            string? PreviousParent,
            Dictionary<string, string>? FullPaths,
            int Priority,
            DateTime? CreateDate,
            DateTime? UpdateDate
    );
}
