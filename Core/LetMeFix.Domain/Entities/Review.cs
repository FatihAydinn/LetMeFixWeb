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
        public string ContractId { get; set; }
        public string CustomerId { get; set; }
        public string ProviderId { get; set; }

        [Range(1,5)]
        public decimal Rate { get; set; }
        public string? ReviewText { get; set; }
        public List<string>? Images { get; set; } //!?
    }
}
