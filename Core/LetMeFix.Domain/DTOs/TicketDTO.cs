﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.DTOs
{
    public class TicketDTO
    {
        public string Title { get; set; } //!?
        public string Description { get; set; }
        public long Price { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }

    }
}
