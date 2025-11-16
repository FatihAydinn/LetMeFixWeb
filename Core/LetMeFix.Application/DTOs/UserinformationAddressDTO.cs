using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class UserinformationAddressDTO
    {
        public string UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string? Neighborhood { get; set; }
        public string? Address { get; set; }
    }
}
