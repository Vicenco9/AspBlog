using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Domain
{
    public class Rate : Entity
    {
        public float Number { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
