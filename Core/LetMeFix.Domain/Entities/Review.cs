using LetMeFix.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Review : BaseEntity
    {
        public string JobId { get; set; }
        public string CustomerId { get; set; }
        public string ProviderId { get; set; }

        [Range(1,5)]
        public decimal Rate { get; set; }
        public string? ReviewText { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<string>? Images { get; set; } //!?
    }
}
