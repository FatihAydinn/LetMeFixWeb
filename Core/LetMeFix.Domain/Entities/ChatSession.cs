using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class ChatSession : BaseEntity
    {
        public string? OfferId { get; set; }
        public string JobId { get; set; }

        //Participants
        public string ProviderId { get; set; }
        public string CustomerId { get; set; }

        public bool IsChatClosed { get; set; } = false;

        public List<MessageContent>? MessageContent { get; set; }
    }
    
    public class MessageContent
    {
        public string? MessageId { get; set; }
        public string Type { get; set; }
        public string SenderId { get; set; }
        public string Content { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public DateTime SentDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public List<PreviousMessages>? PreviousMessages { get; set; }
    }

    public class PreviousMessages
    {
        public string EditedContent { get; set; }
        public DateTime EditDate { get; set; }
    }
}
