using FluentValidation;
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
    public class EfCreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ProjekatContext _context;
        private readonly CreateCategoryValidator _validator;
        public EfCreateCategoryCommand(ProjekatContext context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 1;
        public string Name => "Create category EF";

        public void Execute(CategoryDto request)
        {
            throw new NotImplementedException();
        }

        void ICommand<CategoryDto>.Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var category = new Category
            {
                Name = request.Name
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}