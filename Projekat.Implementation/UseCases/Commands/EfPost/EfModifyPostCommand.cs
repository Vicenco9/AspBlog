using Projekat.Application.Exceptions;
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
using Projekat.Application.UseCases.Commands.Post;

namespace Projekat.Implementation.UseCases.Commands.EfPost
{
    public class EfModifyPostCommand : IModifyPostCommand
    {
        private readonly ProjekatContext _context;
        private readonly ModifyPostValidator _validator;
        public EfModifyPostCommand(ProjekatContext context, ModifyPostValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 6;

        public string Name => "Modify post EF";

        void ICommandUpdate<PostDto, int>.Execute(PostDto request, int id)
        {
            _validator.ValidateAndThrow(request);
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                throw new EntityNotFoundException(id, typeof(User));
            }
            post.Title = request.Title;
            post.Text = request.Text;
            post.PictureId = request.PictureId;
            post.UserId = request.UserId;
            _context.SaveChanges();
        }
    }
}
