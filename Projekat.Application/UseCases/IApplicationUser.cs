using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases
{
    public interface IApplicationUser
    {
        public int Id { get; }
        public string Indenty { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
