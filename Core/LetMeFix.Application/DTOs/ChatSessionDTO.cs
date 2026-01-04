using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class ChatSessionDTO
    {
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public string? OfferId { get; set; }
        public string JobId { get; set; }

        public string ProviderId { get; set; }
        public string CustomerId { get; set; }

        public bool IsChatClosed { get; set; } = false;

        public List<MessageContent>? MessageContent { get; set; }
    }
}
