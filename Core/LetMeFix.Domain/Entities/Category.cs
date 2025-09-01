using LetMeFix.Domain.Common;
using LetMeFix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Domain.Entities
{
    public class Category /*: EntityBase*/
    {
        public Category()
        {
            SubCategories = new HashSet<Category>();
            Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int Priorty { get; set; }
        public bool IsActive { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        //public Ticket Ticket { get; set; }
    }
}
