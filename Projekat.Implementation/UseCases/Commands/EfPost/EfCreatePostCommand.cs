using FluentValidation;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using Projekat.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfPost
{
    public class EfCreatePostCommand : ICreatePostCommand
    {
        private readonly ProjekatContext _context;
        private readonly CreatePostValidator _validator;

        public EfCreatePostCommand(ProjekatContext context, CreatePostValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Create post EF";

        public void Execute(CreatedPostDto request)
        {
            _validator.ValidateAndThrow(request);
            var post = new Post
            {
                Text = request.Text,
                Title = request.Title,
                PictureId = request.PictureId,
                UserId = request.UserId
            };
            _context.Posts.Add(post);
            _context.SaveChanges();
            foreach (var c in request.Category)
            {
                var categories = new CategoryPost
                {
                    PostId = post.Id,
                    CategoryId = c.Id
                };
                _context.CategoryPost.Add(categories);
                _context.SaveChanges();
            }
        }
    }
}
