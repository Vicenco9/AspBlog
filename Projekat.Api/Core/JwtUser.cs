using Projekat.Application.UseCases;
using System.Collections.Generic;

namespace Projekat.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public int Id { get; set; }

        public string Indenty { get; set; }

        public IEnumerable<int> AllowedUseCases { get; set; }
    }
}
