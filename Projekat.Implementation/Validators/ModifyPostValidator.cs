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
    public class ModifyPostValidator : AbstractValidator<PostDto>
    {
        private readonly ProjekatContext _context;
        public ModifyPostValidator(ProjekatContext context)
        {
            _context = context;
        }
        public ModifyPostValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title can't be empty field!")
            .Must(x => !_context.Posts.Any(user => user.Title == x))
             .WithMessage("Username is already taken.");


            RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text can't be empty field!");
        }
    }
}
