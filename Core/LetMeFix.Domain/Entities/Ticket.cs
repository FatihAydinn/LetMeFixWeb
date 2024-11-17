using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Ticket
    {
        public string Title { get; set; } //!?
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string status { get; set; }

        public ICollection<Category> Categories { get; set; } //one-to many relation
        //public string ImagePath { get; set; }
    }
}
