using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases.Queries;
using Projekat.Application.UseCases.Searches;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Queries
{
    public class EfGetPostsQuery : IGetPostsQuery
    {
        private readonly ProjekatContext _context;
        public EfGetPostsQuery(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 14;

        public string Name => "Post search EF";

        public PagedResponse<PostDto> Execute(PostSearch search)
        {

            var query = _context.Posts.AsQueryable();
            if (!string.IsNullOrEmpty(search.Title) || !string.IsNullOrWhiteSpace(search.Title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(search.Title.ToLower()));
            }
            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<PostDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query
                .Skip(skipCount)
                .Take(search.PerPage)
                .Select(x => new PostDto
                {
                    Title = x.Title,
                    Text = x.Text,
                    Categories = x.CategoryPost.Select(cp => new CategoryDto 
                    {
                        Name = cp.Category.Name,
                        Id = cp.Category.Id 
                    }).ToList(),
                    PictureId = x.PictureId,
                    UserId = x.UserId,
                })
                .ToList()
            };
            return response;
        }
    }
}
