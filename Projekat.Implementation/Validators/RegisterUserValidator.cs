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
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(ProjekatContext context)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
               .NotEmpty();

            RuleFor(x => x.Password)
               .NotEmpty()
               .MinimumLength(6);

            RuleFor(x => x.Username)
                 .NotEmpty()
                 .MinimumLength(4)
                 .Must(x => !context.Users.Any(user => user.Username == x))
                 .WithMessage("Username is already taken");

            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()
               .Must(x => !context.Users.Any(user => user.Email == x))
               .WithMessage("Email is already taken");
        }
    }
}
