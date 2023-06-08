using Projekat.Application.Exceptions;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.User;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekat.Implementation.UseCases.Commands.EfUser
{
    public class EfDeleteUserCommand : IDeleteUserCommand
    {
        private readonly ProjekatContext _context;
        public EfDeleteUserCommand(ProjekatContext context)
        {
            _context = context;
        }
        public int Id => 10;

        public string Name => "Delete user EF";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);
            if (user == null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }
            //bc of Soft Delete
           // _context.Users.Remove(user);
            user.DeletedAt = DateTime.Now;
            user.IsActive = false;
            user.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
