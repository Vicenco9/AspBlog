using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.Queries
{
    public abstract class PagedSearch
    {
        public int PerPage { get; set; } = 10;
        public int Page { get; set; } = 1;
    }
}
