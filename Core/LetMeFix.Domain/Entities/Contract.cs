using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Contract
    {
        public string ProviderId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Deadline { get; set; }
        public JobStatus Status { get; set; }
        public bool GetNotifications { get; set; }

        public string? Neighborhood { get; set; }
        public string? Address { get; set; }

        public Reviews? Review { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public decimal?  Tip { get; set; }
    }
}
