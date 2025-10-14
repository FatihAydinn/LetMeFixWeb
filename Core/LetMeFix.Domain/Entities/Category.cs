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
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public int Priority { get; set; }
        public int Level => Id.Length / 3;
    }
}
