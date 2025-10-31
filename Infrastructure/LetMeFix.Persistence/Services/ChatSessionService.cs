using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Services
{
    public class ChatSessionService : BaseService<ChatSession>
    {
        public ChatSessionService(IMongoDatabase database) : base (database, "Chats")
        { }

        public async Task AddAsync(ChatSession entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<ChatSession> GetByChatIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MessageContent> GetMessageById(string chatsession, string messageid)
        {
            var chat = await _collection.Find(x => x.Id ==  chatsession).FirstOrDefaultAsync();
            var msg = chat.MessageContent?.Find(x => x.MessageId == messageid);
            return msg;
        }

        public async Task<List<ChatSession>> GetByUserId(string userid)
        {
            return await _collection.Find(x => x.ProviderId == userid || x.CustomerId == userid).ToListAsync();
        }

        public async Task UpdateAsync(ChatSession entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task PushMessage(string chatSessionId, MessageContent message)
        {
            var filter = Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId);
            var update = Builders<ChatSession>.Update.Push(x => x.MessageContent, message);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteMessage(string chatSessionId, string messageId)
        {
            var filter = Builders<ChatSession>.Filter.And(
                Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId),
                Builders<ChatSession>.Filter.ElemMatch(x => x.MessageContent, Builders<MessageContent>.Filter.Eq(z => z.MessageId, messageId))
                );
            var update = Builders<ChatSession>.Update.Set("MessageContent.$.Status", "deleted");
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task EditMessage(string editedmsg, string chatSessionId, string messageId)
        {
            PreviousMessages previousMessage = new PreviousMessages();
            var content = await _collection.Find(x => x.Id == chatSessionId).Project(x => x.MessageContent.FirstOrDefault(k => k.MessageId == messageId).Content).FirstOrDefaultAsync();

            previousMessage.EditedContent = content.ToString();
            previousMessage.EditDate = DateTime.Now;

            var filter = Builders<ChatSession>.Filter.And(
                Builders<ChatSession>.Filter.Eq(x => x.Id, chatSessionId),
                Builders<ChatSession>.Filter.ElemMatch(x => x.MessageContent, Builders<MessageContent>.Filter.Eq(z => z.MessageId, messageId))                
                );
            var update = Builders<ChatSession>.Update
                .Set("MessageContent.$.Status", "edited")
                .Push("MessageContent.$.PreviousMessages", previousMessage)
                .Set("MessageContent.$.Content", editedmsg);
            await _collection.UpdateOneAsync(filter, update);
        }
    }
}
