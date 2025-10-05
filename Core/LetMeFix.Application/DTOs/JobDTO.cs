using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class JobDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; } //!?
        public string Description { get; set; }
        public long Price { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }

    }
}
