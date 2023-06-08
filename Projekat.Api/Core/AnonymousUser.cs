using Projekat.Application.UseCases;
using System.Collections.Generic;

namespace Projekat.Api.Core
{
    public class AnonymousUser : IApplicationUser
    {
        public int Id => 0;

        public string Indenty => "Anonymus";

        public IEnumerable<int> AllowedUseCases => new List<int> { 15 };
    }
}
