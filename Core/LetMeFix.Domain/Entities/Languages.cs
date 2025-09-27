using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Languages
    {
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageEmoji { get; set; }
    }
}
