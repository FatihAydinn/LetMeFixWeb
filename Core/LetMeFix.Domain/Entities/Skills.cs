using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Skills : BaseEntity
    {
        public string SkillTitle { get; set; }
        public List<string> RelatedCategories { get; set; }
    }
}
