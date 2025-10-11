using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Offers
    {
        public string? Id { get; set; }
        public string JobId { get; set; }
        public string CustomerId { get; set; }
        public decimal Price { get; set; }
        public string? TimeType { get; set; }
        public int? EstimatedDuration { get; set; }
        public bool IsAccepted { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Neighborhood { get; set; }
        public string? Address { get; set; }
    }
}
