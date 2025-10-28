using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
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
    }
}
