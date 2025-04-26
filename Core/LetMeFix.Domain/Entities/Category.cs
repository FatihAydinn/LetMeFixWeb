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
        public Category() { }
        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Name = name;
            Priorty = priorty;
        }
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priorty { get; set; }

        //public Ticket Ticket { get; set; }
    }
}
