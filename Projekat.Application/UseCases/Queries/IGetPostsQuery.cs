using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.UseCases.Queries
{
    public interface IGetPostsQuery : IQuery<PostSearch, PagedResponse<PostDto>>
    {
    }
}
