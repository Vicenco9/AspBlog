using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual ICollection<UserUseCase> UserUseCases { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
        public virtual ICollection<Rate> Rates { get; set; } = new HashSet<Rate>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    }
}
