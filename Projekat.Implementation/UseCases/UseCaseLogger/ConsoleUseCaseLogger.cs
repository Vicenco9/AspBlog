using Newtonsoft.Json;
using Projekat.Application.UseCases;
using Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.UseCaseLogger
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, IApplicationUser user, object data)
        {
            Console.WriteLine($"{DateTime.Now}: {user.Indenty} is typing to execute {useCase.Name} using data:" + $"{JsonConvert.SerializeObject(data)}");
        }
    }
}
