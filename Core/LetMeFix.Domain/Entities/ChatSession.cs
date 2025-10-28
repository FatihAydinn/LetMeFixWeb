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
        public bool IsChatCloded { get; set; }

        public List<MessageContent> MessageContent { get; set; }
    }
    
    public class MessageContent
    {
        public string Type { get; set; }
        public string SenderId { get; set; }
        public string? Content { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public DateTime SentDate { get; set; }
    }
}
