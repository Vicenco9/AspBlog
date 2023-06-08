using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<CategoryPost> CategoryPost { get; set; } = new HashSet<CategoryPost>();
    }
}
