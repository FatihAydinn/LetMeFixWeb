using LetMeFix.Domain.Common;
using LetMeFix.Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Category /*: EntityBase*/
    {
        public Category()
        {
            SubCategories = new HashSet<Category>();
            Tickets = new HashSet<Ticket>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? ParentId { get; set; }
        public int Priorty { get; set; }
        public bool IsActive { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        //public Ticket Ticket { get; set; }
    }
}
