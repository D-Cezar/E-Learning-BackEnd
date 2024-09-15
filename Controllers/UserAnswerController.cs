using E_Learning.DBElements.Commands;
using E_Learning.DTOs.Responses;
using E_Learning.MediatorComponents.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.Controllers
{
    [Controller]
    [Route("/Answers")]
    [Authorize]
    public class UserAnswerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAnswerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SubmitAnswerAsync([FromBody] AddAnswer submitedAnswer)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (userId != submitedAnswer.UserId)
                return Unauthorized();

            var submitedObject = await _mediator.Send(submitedAnswer);

            if (submitedObject == null)
                return BadRequest();

            return Ok(submitedObject);
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<List<LastAnswerDTO>>> GetLastAnswers([FromRoute] int courseId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (courseId == null)
                return BadRequest();

            var lastAnswerRequest = new LastAnswers() { UserId = int.Parse(userId), CourseId = courseId };
            var requestAnswer = await _mediator.Send(lastAnswerRequest);

            return Ok(requestAnswer);
        }
    }
}