using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class UserInformationSocialsDTO
    {
        public string UserId { get; set; }
        public string? Resume { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public string? Github { get; set; }
        public string? Website { get; set; }
        public string? Instagram { get; set; }
    }
}
