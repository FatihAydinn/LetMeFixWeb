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
    class ReportRepository : BaseRepository<Reports>
    {
        public ReportRepository(IMongoDatabase database) : base (database, "Reports") { }
    }
}
