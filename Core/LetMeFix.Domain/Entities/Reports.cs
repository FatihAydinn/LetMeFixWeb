using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Reports : BaseEntity
    {
        public string ReportType { get; set; }
        public string UserId { get; set; }
        public string ReportedUserId { get; set; }

        //for job
        public string? JobId { get; set; }

        //for rewiew
        //jobId
        public string? ReviewId { get; set; }

        //for chat
        public string? ChatRoomId { get; set; }

        public string Reason { get; set; }
        public string Result { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }

    public enum ReportStatus
    {
        Waiting = 1,
        UnderReview = 2,
        Concluded = 3
    }
}
