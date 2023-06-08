using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Category;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfCategory
{
    public class EfGetCategoryQuery : IGetCategoryQuery
    {
        private readonly ProjekatContext _context;

        public EfGetCategoryQuery(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 9;

        public string Name => "Get category EF";


        public CategoryDto Execute(int search)
        {
            var category = _context.Categories.Find(search);
            if (category == null)
            {
                throw new EntityNotFoundException(search, typeof(Category));
            }
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
            return response;
        }
    }
}
