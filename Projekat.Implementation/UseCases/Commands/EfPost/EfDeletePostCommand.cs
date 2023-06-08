using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfPost
{
    public class EfDeletePostCommand : IDeletePostCommand
    {
        private readonly ProjekatContext _context;
        public EfDeletePostCommand(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 5;

        public string Name => "Delete post EF";

        public void Execute(int request)
        {
            var post = _context.Posts.Find(request);
            if (post == null)
            {
                throw new EntityNotFoundException(request, typeof(Post));
            }
            //bc of Soft Delete
            //_context.Posts.Remove(post);
            post.DeletedAt = DateTime.Now;
            post.IsActive = false;
            post.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
