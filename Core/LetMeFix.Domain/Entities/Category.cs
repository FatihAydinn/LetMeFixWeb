using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Dictionary<string, string> Names { get; set; } = new();
        public string? PreviousParent { get; set; }
        public Dictionary<string, string> FullPaths { get; set; } = new();
        public int Priority { get; set; }
        //public int Priority => Id.Length / 3;
    }
}
