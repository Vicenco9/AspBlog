using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Comment;
using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat.Implementation.Validators;
using FluentValidation;

namespace Projekat.Implementation.UseCases.Commands.EfComment
{
    public class EfModifyCommentCommand : IModifyCommentCommand
    {
        private readonly ProjekatContext _context;
        private readonly ModifyCommentValidator _validator;

        public EfModifyCommentCommand(ProjekatContext context, ModifyCommentValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 17;

        public string Name => "Modify comment EF";

        void ICommandUpdate<CommentDto, int>.Execute(CommentDto request, int id)
        {
            _validator.ValidateAndThrow(request);
            var comment = _context.Comments.Find(id);
            if (comment == null)
            {
                throw new EntityNotFoundException(id, typeof(Comment));
            };
            comment.TextComment = request.TextComment;
            comment.PostId = request.PostId;
            comment.UserId = request.UserId;
            _context.SaveChanges();
        }
    }
}
