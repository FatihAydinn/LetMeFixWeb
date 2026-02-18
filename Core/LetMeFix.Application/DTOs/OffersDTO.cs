using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record OffersDTO(
        string? Id,
        DateTime? CreateDate,
        DateTime? UpdateDate,
        string JobId,
        string CustomerId,
        decimal Price,
        string? TimeType,
        int? EstimatedDuration,
        bool IsAccepted,
        int JobVersion,
        string? Country,
        string? City,
        string? District,
        string? Neighborhood,
        string? Address
    );
}
