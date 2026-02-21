using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record ChatSessionDTO(
        string? Id,
        string? OfferId,
        string JobId,
        string ProviderId,
        string CustomerId,
        bool IsChatClosed,
        List<MessageContentDTO>? MessageContent 
    );
}
