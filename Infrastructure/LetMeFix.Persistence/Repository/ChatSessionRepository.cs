using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Persistence.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Repository
{
    class ChatSessionRepository : BaseRepository<ChatSession>
    {
        public ChatSessionRepository(IMongoDatabase database) : base (database, "Chats") { }
    }
}
