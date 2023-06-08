using Newtonsoft.Json;
using Projekat.Application.UseCases;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.UseCaseLogger
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly ProjekatContext _context;

        public DatabaseUseCaseLogger(ProjekatContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationUser user, object useCaseData)
        {
            _context.UseCaseLogs.Add(new Domain.UseCaseLog
            {
                Actor = user.Indenty,
                Data = JsonConvert.SerializeObject(useCaseData),
                Date = DateTime.UtcNow,
                UseCaseName = useCase.Name
            });
            _context.SaveChanges();
        }
    }
}
