using LetMeFix.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; } //!?
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public long Price { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }

        public Category Categories { get; set; } //one-to many relation
        //public ICollection<Category> Categories { get; set; } //one-to many relation

        //publiInfrastructurec string ImagePath { get; set; } 
    }
}
