using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetMeFix.Application.DTOs
{
    public class CategoryDTO /*: EntityBase*/
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int Priorty { get; set; }
        public bool IsActive { get; set; }
    }
}
