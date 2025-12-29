using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Interfaces
{
    public interface IChatSessionService
    {
        Task<PagedResult<ChatSession>> GetChatsByUserId(PagedRequest request, string userid);
        Task<string> GetMessageById(string chatsession, string messageid);
        Task PushMessage(string chatSessionId, MessageContent message);
        Task DeleteMessage(string chatSessionId, string messageId);
        Task EditMessage(string editedmsg, string chatSessionId, string messageId);
    }
}
