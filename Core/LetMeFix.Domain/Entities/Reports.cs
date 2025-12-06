using LetMeFix.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    class Reports : EntityBase
    {
        public string ReportType { get; set; }
        public string UserId { get; set; }

        //for job
        public string? JobId { get; set; }
        public string? JobPosterId { get; set; }

        //for rewiew
        //jobId
        public string? ReviewId { get; set; }
        public string? ReviewSenderId { get; set; }

        //for chat
        public string? ChatRoomId { get; set; }
        public string? MessageSenderId { get; set; }

        public string Reason { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }

    public enum ReportStatus
    {
        Waiting = 1,
        UnderReview = 2,
        Concluded = 3
    }
}
