using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Picture;
using Projekat.Application.UseCases.DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public PictureController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<PictureController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PictureController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PictureController>
        [HttpPost]
        public IActionResult Post([FromForm] PictureDto dto, [FromServices] ICreatePictureCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<PictureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PictureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
