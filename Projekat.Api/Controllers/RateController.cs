using Microsoft.AspNetCore.Mvc;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Post;
using Projekat.Application.UseCases.Commands.Rate;
using Projekat.Application.UseCases.DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RateController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<RateController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RateController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RateController>
        [HttpPost]
        public IActionResult Post(int id, [FromBody] RateDto dto, [FromServices] IRatePostCommand command)
        {
            _executor.ExecuteCommandRate(command, dto, id);
            return NoContent();
        }

        // PUT api/<RateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RateController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
