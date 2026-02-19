using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public record UserInformationsDTO(
        string? Id,
        string? AboutMe,
        string Name,
        string LastName,
        string Email,
        string? MobileNumber,
        string? ProfilePic,
        string Gender,
        DateTime BirthDate,
        DateTime RegistrationDate,
        bool IsActive,
        List<string> Roles,
        bool NotificationSettings,
        List<string> Skills,
        string AppLanguage,

        //address section
        string Country,
        string City,
        string District,
        string? Neighborhood,
        string? Address,

        //summary
        decimal AvrageRate,
        List<string> Reviews,
        List<string> CompletedJobs,
        int CompletedJobCount,
        List<string> PreferredLanguages,
        string Profession,

        //socials
        string? Resume,
        string? LinkedIn,
        string? Twitter,
        string? Github,
        string? Website,
        string? Instagram,

        bool ShowInformationPublic,

        DateTime? CreateDate,
        DateTime? UpdateDate
    );
}
