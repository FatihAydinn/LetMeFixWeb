using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record ReportsDTO(
        string? Id,
        DateTime? CreateDate,
        DateTime? UpdateDate,
        string ReportType,
        string UserId,
        string ReportedUserId,
        string? JobId,
        string? ReviewId,
        string? ChatRoomId,
        string? Reason,
        string? Result,
        ReportStatus ReportStatus
    );
}
