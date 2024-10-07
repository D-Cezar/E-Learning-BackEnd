using E_Learning.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [Controller]
    [Authorize]
    [Route("[controller]")]
    public class CourseSectionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseSectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch]
        public async Task<ActionResult<bool>> EditSectionDescriptionOrTitle([FromBody] EditCourseSectionDescription command)
        {
            if (command == null || string.IsNullOrWhiteSpace(command.Text))
                return BadRequest(false);

            var result = await _mediator.Send(command);

            return result ? Ok(true) : BadRequest(false);
        }
    }
}