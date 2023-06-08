using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.Queries
{
    public class PagedResponse<T> where T : class
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int PagedCount => (int)Math.Ceiling((float)TotalCount / ItemsPerPage);
    }
}
