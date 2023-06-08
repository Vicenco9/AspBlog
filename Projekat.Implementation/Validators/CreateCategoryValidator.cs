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
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryValidator(ProjekatContext context)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(x => !context.Categories.Any(c => c.Name == x))
                .WithMessage("Category must have unique name.");

        }
    }
}
