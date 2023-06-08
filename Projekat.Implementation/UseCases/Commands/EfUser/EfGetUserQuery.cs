using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.User;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfUser
{
    public class EfGetUserQuery : IGetUserQuery
    {
        private readonly ProjekatContext _context;

        public EfGetUserQuery(ProjekatContext context)
        {
            _context = context;
        }

        public int Id => 12;

        public string Name => "Get user EF";

        public UserDto Execute(int search)
        {
            var user = _context.Users.Find(search);
            if (user == null)
            {
                throw new EntityNotFoundException(search, typeof(User));
            }

            var response = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username
            };
            return response;
        }
    }
}
