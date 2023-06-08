using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekat.Domain;
using Projekat.EfDataAccess;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {
        // GET: api/<InitialDataController>
        [HttpGet]
        public IActionResult Get()
        {
            var context = new ProjekatContext();

            if (context.Users.Any())
            {
                return Conflict();
            }

            var users = new List<User>()
            {
                new User {FirstName = "Petar", LastName = "Petrovic", Username = "peca", Email = "peca@gmail.com", Password="peca123"},
                new User {FirstName = "Jovan", LastName = "Lazic", Username = "joca", Email = "joca@gmail.com", Password="joca"},
                new User {FirstName = "Milos", LastName = "Milosevic", Username = "miki", Email = "miki@gmail.com", Password="miki"},
            };

            var pictures = new List<Picture>()
            {
                new Picture { Src = "path1/image1.com", Alt = "image1"},
                new Picture { Src = "path2/image2.com", Alt = "image2"},
                new Picture { Src = "path3/image3.com", Alt = "image3"},
            };

            var posts = new List<Post>()
            {
                new Post { Title = "Denver pobedio Lakerse, Jokic postigao triple-double", Text = "Text 1", Picture = pictures.ElementAt(1), User = users.First(), AverageRate = 4 },
                new Post { Title = "Najukusniji recept za palacinke", Text = "Text 2", Picture = pictures.First(), User = users.ElementAt(1), AverageRate = 2 },
                new Post { Title = "Kako zapoceti svoj biznis.", Text = "Text3", Picture = pictures.ElementAt(2), User = users.ElementAt(2), AverageRate = 5 },
            };

            var categories = new List<Category>()
            {
                new Category { Name = "Sport"},
                new Category { Name = "Nauka"},
                new Category { Name = "Biznis"},
                new Category { Name = "Recepti"},
            };

            var categoryPost = new List<CategoryPost>()
            {
                new CategoryPost { Category = categories.First(), Post = posts.First() },
                new CategoryPost { Category = categories.ElementAt(3), Post = posts.ElementAt(1) },
                new CategoryPost { Category = categories.ElementAt(2), Post = posts.ElementAt(2) },
            };

            var rates = new List<Rate>()
            {
                new Rate {Number = 4, User = users.First(), Post = posts.ElementAt(2)},
                new Rate {Number = 1, User = users.ElementAt(1), Post = posts.First()},
                new Rate {Number = 4, User = users.First(), Post = posts.ElementAt(1)},
            };

            var comments = new List<Comment>()
            {
                new Comment {TextComment = "Komentar 1", User = users.ElementAt(2), Post = posts.First()},
                new Comment {TextComment = "Komentar 2", User = users.ElementAt(1), Post = posts.ElementAt(1)},
                new Comment {TextComment = "Komentar 3", User = users.ElementAt(1), Post = posts.ElementAt(0)},
                new Comment {TextComment = "Komentar 4", User = users.ElementAt(1), Post = posts.ElementAt(0)},
            };

            context.Users.AddRange(users);
            context.Pictures.AddRange(pictures);
            context.Posts.AddRange(posts);
            context.Categories.AddRange(categories);
            context.CategoryPost.AddRange(categoryPost);
            context.Rates.AddRange(rates);
            context.Comments.AddRange(comments);

            context.SaveChanges();

            return StatusCode(201);
        }
    }
}
