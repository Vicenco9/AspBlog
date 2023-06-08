using Projekat.Application.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.Searches
{
    public class PostSearch : PagedSearch
    {
        public string Title { get; set; }
    }
}
