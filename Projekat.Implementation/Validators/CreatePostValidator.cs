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
    public class CreatePostValidator : AbstractValidator<CreatedPostDto>
    {
        public CreatePostValidator(ProjekatContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .Must(x => !context.Posts.Any(c => c.Title == x))
                .WithMessage("Title must be unique!");

            RuleFor(x => x.Text)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Text has minimum of 6 characters.");

        }
    }
}
