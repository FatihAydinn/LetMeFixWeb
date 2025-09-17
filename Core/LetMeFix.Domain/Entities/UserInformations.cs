using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class UserInformations
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string? ProfilePic { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        //address section
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string? Neighborhood { get; set; }
        public string? Address { get; set; }

        public string? AboutMe { get; set; }
        public string Profession { get; set; }
        public string? Resume { get; set; }
        public string? LinkedIn { get; set; }
        public string? Instragram { get; set; }
        public bool ShowInformation { get; set; } = true;
    }
}
