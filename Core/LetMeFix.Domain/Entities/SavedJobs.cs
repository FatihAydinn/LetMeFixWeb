using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class SavedJobs : BaseEntity
    {
        public string UserId { get; set; }
        public string JobId { get; set; }
        public string? Note { get; set; }
    }
}
