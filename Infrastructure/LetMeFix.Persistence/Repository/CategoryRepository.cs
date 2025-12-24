using LetMeFix.Application.Interfaces;
using LetMeFix.Domain.Entities;
using LetMeFix.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Persistence.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IMongoDatabase database) : base(database, "CategoryStages") { }
    }
}
