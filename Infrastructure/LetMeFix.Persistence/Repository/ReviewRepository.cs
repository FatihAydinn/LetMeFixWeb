using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        public ReviewRepository(IMongoDatabase database) : base(database, "Reviews") { }
    }
}
