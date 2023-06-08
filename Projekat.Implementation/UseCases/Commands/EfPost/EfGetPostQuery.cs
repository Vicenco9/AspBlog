using Microsoft.EntityFrameworkCore;
using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases.Queries;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfPost
{
    public class EfGetPostQuery : IGetPostQuery
    {
        private readonly ProjekatContext _context;
        public EfGetPostQuery(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 7;

        public string Name => "Get one post EF";

        public PostDto Execute(int request)
        {
            var post = _context.Posts.Include(p => p.CategoryPost).ThenInclude(p => p.Category).FirstOrDefault(p => p.Id == request);

            if (post == null)
            {
                throw new EntityNotFoundException(request, typeof(Post));
            }
            var rates = _context.Rates.Where(x => x.PostId == post.Id);
            var avg = rates.Select(x => x.Number);
            var comment = _context.Comments.Where(x => x.PostId == post.Id);
            var user = _context.Users.Find(post.UserId);
            var response = new PostDto
            {
                Text = post.Text,
                Title = post.Title,
                PictureId = post.PictureId,
                UserId = post.UserId,
                Categories = post.CategoryPost.Select(cp => new CategoryDto
                {
                    Name = cp.Category.Name,
                    Id = cp.Category.Id
                }).ToList(),

            };
            return response;
        }

    }
}
