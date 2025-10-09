using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class WorkBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string? Id { get; set; }
        public string ProviderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public bool IsRemoteJob { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }

        public string? TimeType { get; set; }
        public int? EstimatedDuration { get; set; }

        public PaymentType PaymentType { get; set; }

    }
}
