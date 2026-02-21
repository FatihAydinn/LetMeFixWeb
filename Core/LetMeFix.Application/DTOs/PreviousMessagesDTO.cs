using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record PreviousMessagesDTO(
        string? EditedContent,
        DateTime? EditDate
    );
}
