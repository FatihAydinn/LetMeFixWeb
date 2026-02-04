using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Job : WorkBase
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

    public enum ServiceType
    {
        ServiceProvider = 1,
        ServiceRecipient = 2
    }

    public enum JobStatus
    {
        Waiting = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        Expired = 5,
        Deleted = 6
    }

    public class Socials
    {
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
        public string WebSite { get; set; }
    }

    public enum PaymentType
    {
        Cash = 1,
        CreditCard = 2,
        Crypto = 3,
        Checks = 4,
        BankTransfer = 5
    }

    public class CategoryInfo
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int Priorty { get; set; }
        public bool IsActive { get; set; }
    }
}
