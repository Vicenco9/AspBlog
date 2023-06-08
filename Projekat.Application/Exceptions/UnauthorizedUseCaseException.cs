using Projekat.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.Exceptions
{
    public class UnauthorizedUseCaseException : Exception
    {
        public UnauthorizedUseCaseException(IUseCase useCase, IApplicationUser user) : base($"User with an id of{user.Id} - {user.Indenty} tryed to execute {useCase.Name}.")
        {

        }
    }
}
