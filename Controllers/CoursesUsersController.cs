using E_Learning.MiddleComponents.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using E_Learning.MediatorComponents.Commands;

namespace E_Learning.Controllers
{
    [Controller]
    [Route("/[controller]")]
    [Authorize]
    public class CoursesUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Enroll")]
        public async Task<ActionResult<bool>> AddEnrollDate([FromBody] Enroll command)
        {
            var successCreation = await _mediator.Send(command);

            if (!successCreation)
                return BadRequest(successCreation);

            return Ok(successCreation);
        }

        [HttpPut]
        [Route("UpdateCourseUserInfo")]
        public async Task<ActionResult<bool>> UpdateCourseUser([FromBody] UpdateCourseUser update)
        {
            if (update.CourseId == null || update.UserId == null)
                return BadRequest(false);

            var putResult = await _mediator.Send(update);

            return putResult ? Ok(putResult) : BadRequest(putResult);
        }

        [HttpPatch]
        [Route("AddSession")]
        public async Task<ActionResult<bool>> AddTimeSession([FromBody] AddSession addSession)
        {
            var corectUpdate = await _mediator.Send(addSession);
            if (!corectUpdate)
                return BadRequest(false);

            return Ok(corectUpdate);
        }

        [HttpPatch]
        [Route("UpdateCompletion")]
        public async Task<ActionResult<bool>> CompletedSection([FromBody] UpdateCompletedSections update)
        {
            var correctUpdate = await _mediator.Send(update);

            if (correctUpdate)
                return Ok(true);

            return BadRequest(correctUpdate);
        }
    }
}