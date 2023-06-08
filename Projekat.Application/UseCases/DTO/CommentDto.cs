using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.DTO
{
    public class CommentDto
    {
        public string TextComment { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}