using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Translations : BaseEntity
    {
        public string Key { get; set; }
        public string LanguageId { get; set; }
        public string Content { get; set; }
    }
}
