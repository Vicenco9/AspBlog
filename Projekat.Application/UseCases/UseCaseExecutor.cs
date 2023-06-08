using Projekat.Application.Exceptions;
using Projekat.Application.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases
{
    public class UseCaseExecutor
    {
        private readonly IApplicationUser user;
        private readonly IUseCaseLogger logger;
        private readonly IExceptionLogger exceptionLogger;

        public UseCaseExecutor(IApplicationUser user, IUseCaseLogger logger, IExceptionLogger exceptionLogger)
        {
            this.user = user;
            this.logger = logger;
            this.exceptionLogger = exceptionLogger;
        }
        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            try
            {
                logger.Log(command, user, request);
                if (!user.AllowedUseCases.Contains(command.Id))
                {
                    throw new UnauthorizedUseCaseException(command, user);
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                command.Execute(request);

                stopwatch.Stop();

                Console.WriteLine(command.Name + " Duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            catch (Exception ex)
            {
                exceptionLogger.Log(ex);
                throw;
            }
        }
        public void ExecuteCommandUpdate<TRequest>(ICommandUpdate<TRequest, int> command, TRequest request, int id)
        {
            try
            {
                logger.Log(command, user, id);
                if (!user.AllowedUseCases.Contains(command.Id))
                {
                    throw new UnauthorizedUseCaseException(command, user);
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                command.Execute(request, id);

                stopwatch.Stop();

                Console.WriteLine(command.Name + " Duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            catch (Exception ex)
            {
                exceptionLogger.Log(ex);
                throw;
            }
        }
        public void ExecuteCommandRate<TRequest>(ICommandRate<TRequest, int> command, TRequest request, int id)
        {
            try
            {
                logger.Log(command, user, id);
                if (!user.AllowedUseCases.Contains(command.Id))
                {
                    throw new UnauthorizedUseCaseException(command, user);
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                command.Execute(request, id);

                stopwatch.Stop();

                Console.WriteLine(command.Name + " Duration: " + stopwatch.ElapsedMilliseconds + " ms.");
            }
            catch (Exception ex)
            {
                exceptionLogger.Log(ex);
                throw;
            }
        }
        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            try
            {
                logger.Log(query, user, search);
                if (!user.AllowedUseCases.Contains(query.Id))
                {
                    throw new UnauthorizedUseCaseException(query, user);
                }
                return query.Execute(search);
            }
            catch (Exception ex)
            {
                exceptionLogger.Log(ex);
                throw;
            }
        }
    }
}
