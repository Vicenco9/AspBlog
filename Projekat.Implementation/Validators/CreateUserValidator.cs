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
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(ProjekatContext context)
        {
            RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .NotEmpty()
                .WithMessage("The FirstName field can't be empty.");

            RuleFor(x => x.LastName)
               .MinimumLength(2)
               .NotEmpty()
               .WithMessage("The LastName field can't be empty.");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("The Email field can't be empty")
               .Must(x => !context.Users.Any(user => user.Email == x))
               .WithMessage("Username is already taken.")
               .EmailAddress();

            RuleFor(x => x.Password)
               .MinimumLength(2)
               .NotEmpty()
               .WithMessage("The Password field can't be empty");

            RuleFor(x => x.Username)
               .NotEmpty()
               .WithMessage("The Username field can't be empty")
               .Must(x => !context.Users.Any(user => user.Username == x))
               .WithMessage("Username is already taken");
        }
    }
}
