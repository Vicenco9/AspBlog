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
    public class ModifyCommentValidator : AbstractValidator<CommentDto>
    {
        public ModifyCommentValidator(ProjekatContext context)
        {
            RuleFor(x => x.TextComment)
               .NotEmpty()
               .WithMessage("Comment can't be empty field.");
            RuleFor(x => x.PostId)
               .NotNull()
               .WithMessage("Id of post can't be null.");
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("Id of user not be null.");
        }
    }
}
