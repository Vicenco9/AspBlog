using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekat.Application.UseCases;
using Projekat.Application.UseCases.Commands.Register;
using Projekat.Application.UseCases.DTO;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projekat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RegisterController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // POST api/<RegisterController>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterUserDto dto, [FromServices] IRegisterUserCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<RegisterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegisterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
