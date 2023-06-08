using FluentValidation;
using Projekat.Application.Exceptions;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Category;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using Projekat.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfCategory
{
    public class EfModifyCategoryCommand : IModifyCategoryCommand
    {
        private readonly ProjekatContext _context;
        private readonly ModifyCategoryValidator _validator;

        public EfModifyCategoryCommand(ProjekatContext context, ModifyCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 3;

        public string Name => "Modify category";

        void ICommandUpdate<CategoryDto, int>.Execute(CategoryDto request, int id)
        {
            _validator.ValidateAndThrow(request);
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new EntityNotFoundException(id, typeof(Category));
            }
            category.Name = request.Name;
            _context.SaveChanges();
        }
    }
}
