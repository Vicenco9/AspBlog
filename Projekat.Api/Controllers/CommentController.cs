using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Comment;
using Projekat.Application.UseCases.DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CommentController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<CommentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CommentController>
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto,[FromServices] ICreateCommentCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentDto dto, [FromServices] IModifyCommentCommand command)
        {
            _executor.ExecuteCommandUpdate(command, dto, id);
            return NoContent();
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
