using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Application.UseCases.DTO;
using Projekat.Application.UseCases.Queries;
using Projekat.Application.UseCases.Searches;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public PostController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch search, [FromServices] IGetPostsQuery query)
        {
            return Ok(query.Execute(search));
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetPostQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post([FromBody] CreatedPostDto dto, [FromServices] ICreatePostCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PostDto dto, [FromServices] IModifyPostCommand command)
        {
            _executor.ExecuteCommandUpdate(command, dto, id);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeletePostCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
