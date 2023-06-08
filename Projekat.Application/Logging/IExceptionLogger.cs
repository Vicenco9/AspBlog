using Newtonsoft.Json;
using Projekat.Application.UseCases;
using Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.Logging
{
    public interface IExceptionLogger
    {
        void Log(Exception ex);
    }
}
