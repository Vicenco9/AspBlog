using FluentValidation;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.User;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using Projekat.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfUser
{
    public class EfCreateUserCommand : ICreateUserCommand
    {
        private readonly ProjekatContext _context;
        private readonly CreateUserValidator _validator;
        public EfCreateUserCommand(ProjekatContext context, CreateUserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 8;

        public string Name => "Create user using Ef";

        void ICommand<UserDto>.Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Username = request.Username
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}