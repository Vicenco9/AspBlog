using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Domain
{
    public class Picture : Entity
    {
        public string Src { get; set; }
        public string Alt { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}
