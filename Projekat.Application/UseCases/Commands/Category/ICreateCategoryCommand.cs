using Projekat.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.Commands.Category
{
    public interface ICreateCategoryCommand : ICommand<CategoryDto>
    {
    }
}
