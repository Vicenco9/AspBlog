using Microsoft.EntityFrameworkCore;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Rate;
using Projekat.Application.UseCases.DTO;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Implementation.UseCases.Commands.EfRate
{
    public class EfCreateRateCommand : IRatePostCommand
    {
        private readonly ProjekatContext _context;

        public EfCreateRateCommand(ProjekatContext context)
        {
            _context = context;
        }

        public int Id => 19;

        public string Name => "Rate EF";

        public void Execute(RateDto request, int id)
        {
            var userPost = _context.Rates.Where(x => x.PostId == request.PostId).Select(x => x.UserId);
            if (request.Number > 5)
            {
                throw new ArgumentException("Number must be under 6.");
            }
            if (userPost.Contains(request.UserId))
            {
                throw new ArgumentException("You have already voted.");
            }
            var rate = new Rate
            {
                Number = request.Number,
                UserId = request.UserId,
                PostId = request.PostId
            };
            _context.Rates.Add(rate);
            _context.SaveChanges();
        }
    }
}
