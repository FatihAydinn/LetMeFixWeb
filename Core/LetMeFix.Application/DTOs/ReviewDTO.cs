using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record ReviewDTO(
        string? Id,
        DateTime? CreateDate,
        DateTime? UpdateDate,
        string ContractId,
        string CustomerId,
        string ProviderId,
        string JobId,

        [Range(1,5)]
        decimal Rate,
        string? ReviewText,
        List<string>? Images
    );
}
