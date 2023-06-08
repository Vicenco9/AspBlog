using FluentValidation;
using Projekat.Application.Emails;
using Projekat.Application.UseCases.Commands.Register;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using Projekat.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfRegister
{
    public class EfRegisterUserCommand : IRegisterUserCommand
    {
        private readonly ProjekatContext _context;
        private readonly RegisterUserValidator _validator;
        private readonly IEmailSender _sender;
        public EfRegisterUserCommand(ProjekatContext context, RegisterUserValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 15;

        public string Name => "User Registration";

        public void Execute(RegisterUserDto request)
        {
            var cases = new List<int> { 14, 19, 16 };
            _validator.ValidateAndThrow(request);

            //var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                //Password = hash,
                Password = request.Password,
                Username = request.Username
            };

            _context.Users.Add(user);
            _context.SaveChanges();
           
            foreach (var i in cases)
            {
                var userUseCase = new UserUseCase
                {
                    UseCaseId = i,
                    UserId = user.Id
                };
                _context.Add(userUseCase);
            }
            _context.SaveChanges();

            _sender.Send(new EmailMessageDto
            {
                SendTo = request.Email,
                Subject = "Successfuly registration!",
                Content = "Dear " + request.Username + "\n Please activate your account..."
            });
        }
    }
}
