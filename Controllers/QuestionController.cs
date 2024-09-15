using E_Learning.MediatorComponents.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Learning.Constants;

namespace E_Learning.API.Controllers
{
    [Controller]
    [Authorize]
    [Route("/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddQuestion([FromBody] AddQuestion command)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != Roles.Teacher.ToString())
                return Unauthorized();

            var result = await _mediator.Send(command);
            if (result)
                return Ok(true);
            else
                return BadRequest(false);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateQuestion([FromBody] UpdateQuestion command)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != Roles.Teacher.ToString())
                return Unauthorized();

            var result = await _mediator.Send(command);
            if (result)
                return Ok(true);
            else
                return NotFound(false);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteQuestion(int id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != Roles.Teacher.ToString())
                return Unauthorized();

            var result = await _mediator.Send(new DeleteQuestion { Id = id });
            if (result)
                return Ok(true);
            else
                return NotFound(false);
        }
    }
}