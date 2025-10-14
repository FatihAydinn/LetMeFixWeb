using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class CategoryStages : BaseEntity
    {
        public string Name { get; set; }
        public string? PreviousParent { get; set; }
    }
}
