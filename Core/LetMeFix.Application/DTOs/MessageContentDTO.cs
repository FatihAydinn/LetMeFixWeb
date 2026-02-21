using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record MessageContentDTO(
        string? MessageId,
        string? ChatSessionId,
        string Type,
        string SenderId,
        string Content,
        decimal? Price,
        string? Currency,
        DateTime SentDate,
        string Status,
        List<PreviousMessagesDTO>? PreviousMessages
    );
}
