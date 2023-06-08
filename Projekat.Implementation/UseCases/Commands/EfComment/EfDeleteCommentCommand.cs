using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Comment;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfComment
{
    public class EfDeleteCommentCommand : IDeleteCommentCommand
    {
        private readonly ProjekatContext _context;

        public EfDeleteCommentCommand(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 18;

        public string Name => "Delete comment EF";

        public void Execute(int request)
        {
            var comment = _context.Comments.Find(request);
            if (comment == null)
            {
                throw new EntityNotFoundException(request, typeof(Comment));
            }
            //soft del
            comment.DeletedAt = DateTime.Now;
            comment.IsDeleted = true;
            comment.IsActive = false;
            _context.SaveChanges();
        }
    }
}
