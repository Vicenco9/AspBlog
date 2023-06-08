using Projekat.Application.Exceptions;
using Projekat.Application.UseCases.Commands.Category;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfCategory
{
    public class EfDeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly ProjekatContext _context;

        public EfDeleteCategoryCommand(ProjekatContext context)
        {
            _context = context;
        }

        public int Id => 2;

        public string Name => "Delete category EF";

        public void Execute(int request)
        {
            var category = _context.Categories.Find(request);
            if (category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));
            }
            //Bc of Soft Delete
            //_context.Categories.Remove(category);
            category.DeletedAt = DateTime.Now;
            category.IsDeleted = true;
            category.IsActive = false;
            _context.SaveChanges();
        }
    }
}
