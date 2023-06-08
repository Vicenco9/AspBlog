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
    public class ModifyCategoryValidator : AbstractValidator<CategoryDto>
    {
        public ModifyCategoryValidator(ProjekatContext context)
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .WithMessage("Category can't be empty.")
              .Must(x => !context.Categories.Any(category => category.Name == x))
              .WithMessage("Name is already taken.");
        }
    }
}
