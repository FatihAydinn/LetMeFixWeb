using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class ContractsDTO : WorkBaseDTO
    {
        public string ClientId { get; set; }
        public string JobId { get; set; }
        public string? OfferId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? Deadline { get; set; }
        public JobStatus Status { get; set; }
        public bool GetNotifications { get; set; }

        public string? Neighborhood { get; set; }
        public string? Address { get; set; }

        public string? ReviewId { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public decimal? Tip { get; set; }
    }
}
