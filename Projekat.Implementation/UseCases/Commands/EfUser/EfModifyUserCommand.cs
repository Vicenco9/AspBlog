using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.User;
using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat.Implementation.Validators;
using FluentValidation;

namespace Projekat.Implementation.UseCases.Commands.EfUser
{
    public class EfModifyUserCommand : IModifyUserCommand
    {
        private readonly ProjekatContext _context;
        private readonly ModifyUserValidator _validator;
        public EfModifyUserCommand(ProjekatContext context, ModifyUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 11;

        public string Name => "Modify user EF";

        void ICommandUpdate<UserDto, int>.Execute(UserDto request, int id)
        {
            _validator.ValidateAndThrow(request);
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new EntityNotFoundException(id, typeof(User));
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.Username = request.Username;
            _context.SaveChanges();
        }
    }
}
