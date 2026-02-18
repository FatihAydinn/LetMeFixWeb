using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class JobDTO : WorkBaseDTO
    {
        public bool? IsAvailableAnyTime { get; set; }
        public List<string>? AvailableDays { get; set; }
        public List<string>? AvailableHours { get; set; }
        public List<string>? ReviewIds { get; set; }
        public List<string>? OfferIds { get; set; }

        public bool IsActive { get; set; } = true;
        public int Version { get; set; } = 1;

        //admin
        public string? DeleteReason { get; set; }
    }
}
