using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class UserInformationSummaryDTO
    {
        public string UserId { get; set; }
        public decimal AvrageRate { get; set; }
        public List<string> Reviews { get; set; }
        public List<string> CompletedJobs { get; set; }
        public int CompletedJobCount { get; set; }
        public List<string> PreferredLanguages { get; set; }
        public string Profession { get; set; }
    }
}
