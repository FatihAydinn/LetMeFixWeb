using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Languages
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? LanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageEmoji { get; set; }
    }
}
