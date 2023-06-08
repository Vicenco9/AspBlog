using FluentValidation;
using Projekat.Application.UseCases.Commands.Comment;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using Projekat.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfComment
{
    public class EfCreateCommentCommand : ICreateCommentCommand
    {
        private readonly ProjekatContext _context;
        private readonly CreateCommentValidator _validator;

        public EfCreateCommentCommand(ProjekatContext context, CreateCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "Create comment EF";

        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);
            var comment = new Comment
            {
                TextComment = request.TextComment,
                PostId = request.PostId,
                UserId = request.UserId
            };
            _context.Add(comment);
            _context.SaveChanges();
        }
    }
}