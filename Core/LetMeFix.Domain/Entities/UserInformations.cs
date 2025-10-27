using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class UserInformations : BaseEntity
    {
        public string? AboutMe { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? ProfilePic { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public bool NotificationSettings { get; set; }
        public List<string> Skills { get; set; }
        public string AppLanguage { get; set; }

        //address section
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string? Neighborhood { get; set; }
        public string? Address { get; set; }

        //preferences
        public decimal AvrageRate { get; set; }
        public List<string> Reviews { get; set; }
        public List<string> CompletedJobs { get; set; }
        public int CompletedJobCount { get; set; }
        public List<string> PreferredLanguages { get; set; }
        public string Profession { get; set; }

        //socials
        public string? Resume { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public string? Github { get; set; }
        public string? Website { get; set; }
        public string? Instagram { get; set; }

        public bool ShowInformationPublic { get; set; }
    }
}
