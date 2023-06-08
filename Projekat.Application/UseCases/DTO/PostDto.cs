using Projekat.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.DTO
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int PictureId { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<CategoryDto> Categories { get; set; }
    }

    //{
    //    "title": "Proba Post 5 EF",
    //    "text": "Text EF",
    //    "pictureId": 2,
    //    "userId": 1,
    //    "category": [
    //        {
    //            "id": 1
    //        },
    //        {
    //            "id": 2
    //        }
    //    ]
    //}
    public class CreatedPostDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int PictureId { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Category> Category { get; set; }
    }

}
