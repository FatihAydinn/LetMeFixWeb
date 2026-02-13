using AutoMapper;
using LetMeFix.Application.DTOs;
using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.Services
{
    public class ChatSessionService : IChatSessionService
    {
        private readonly IGenericRepository<ChatSession> _repository;
        private readonly IMapper _mapper;

        public ChatSessionService(IGenericRepository<ChatSession> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ChatSession> CreateChatRoom(ChatSessionDTO dto)
        {
            MessageContent msgc = new MessageContent();
            var chatSession = _mapper.Map<ChatSession>(dto);
            chatSession.Id = Guid.NewGuid().ToString();
            chatSession.MessageContent = dto.MessageContent;
            chatSession.MessageContent[0].MessageId = Guid.NewGuid().ToString();
            chatSession.MessageContent[0].ChatSessionId = chatSession.Id;
            chatSession.MessageContent[0].PreviousMessages = [];
            await _repository.AddAsync(chatSession);
            return chatSession;
        }

        public async Task<string> GetMessageById(string chatsessionId, string messageid)
        {
            var chat = await _repository.GetByIdAsync(chatsessionId);
            var msg = chat.MessageContent?.Find(x => x.MessageId == messageid);
            return msg.Content;
        }

        public async Task<PagedResult<ChatSession>> GetChatsByUserId(PagedRequest request, string userid)
        {
            var filter = Builders<ChatSession>.Filter.Where(x => x.ProviderId == userid || x.CustomerId == userid);
            return await _repository.FindAsync(request, filter);
        }

        public async Task PushMessage(string chatSessionId, MessageContent message)
        {
            message.PreviousMessages = [];

            var filter = Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId);
            var update = Builders<ChatSession>.Update.Push(x => x.MessageContent, message).Set(x => x.UpdateDate, DateTime.Now);
            await _repository.UpdateWithFilter(filter, update);
        }

        //soft delete
        public async Task DeleteMessage(string chatSessionId, string messageId)
        {
            var filter = Builders<ChatSession>.Filter.And(
                Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId),
                Builders<ChatSession>.Filter.ElemMatch(x => x.MessageContent, Builders<MessageContent>.Filter.Eq(z => z.MessageId, messageId))
                );
            var update = Builders<ChatSession>.Update.Set("MessageContent.$.Status", "deleted").Set(x => x.UpdateDate, DateTime.Now);
            await _repository.UpdateWithFilter(filter, update);
        }

        public async Task EditMessage(string editedmsg, string chatSessionId, string messageId)
        {
            PreviousMessages previousMessage = new PreviousMessages();
            var previousMsg = GetMessageById(chatSessionId, messageId);

            previousMessage.EditedContent = previousMsg.Result;
            previousMessage.EditDate = DateTime.Now;

            var filter = Builders<ChatSession>.Filter.And(
                Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId),
                Builders<ChatSession>.Filter.ElemMatch(x => x.MessageContent, Builders<MessageContent>.Filter.Eq(z => z.MessageId, messageId))                
                );
            var update = Builders<ChatSession>.Update
                .Set("MessageContent.$.Status", "edited")
                .Push("MessageContent.$.PreviousMessages", previousMessage)
                .Set("MessageContent.$.Content", editedmsg);
            await _repository.UpdateWithFilter(filter, update);
        }
    }
}
