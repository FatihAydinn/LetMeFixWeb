using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Skills
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? SkillId { get; set; }
        public string SkillTitle { get; set; }
        public List<string> SkillCategories { get; set; }
    }
}
