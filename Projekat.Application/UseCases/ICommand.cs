using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases
{
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest request);
    }
    public interface IQuery<TSearch, TResult> : IUseCase
    {
        TResult Execute(TSearch search);
    }
    public interface IUseCase
    {
        int Id { get; }
        string Name { get; }
    }
    public interface ICommandUpdate<TRequest, TInt> : IUseCase
    {
        void Execute(TRequest request, TInt id);
    }
    public interface ICommandRate<TRequest, TInt> : IUseCase
    {
        void Execute(TRequest request, TInt id);
    }
}
