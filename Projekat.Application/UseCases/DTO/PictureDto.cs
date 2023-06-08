using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.DTO
{
    public class PictureDto
    {
        public IFormFile Image { get; set; }
        public IFormFile Alt { get; set; }
    }
}
