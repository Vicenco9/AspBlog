using FluentValidation;
using Projekat.Application.UseCases.DTO;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.Validators
{
    public class ModifyUserValidator : AbstractValidator<UserDto>
    {
        public ModifyUserValidator(ProjekatContext context)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("LastName is required.");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email is required.")
               .Must(x => !context.Users.Any(user => user.Email == x))
                .WithMessage("Email is alredy taken");


            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage("Email is required.");

            RuleFor(x => x.Username)
              .NotEmpty()
              .WithMessage("Email is required.")
              .Must(x => !context.Users.Any(user => user.Username == x))
              .WithMessage("Username is already taken.");


        }
    }
}
