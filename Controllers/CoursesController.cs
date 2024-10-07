using E_Learning.Commands;
using E_Learning.DBElements.Queries;
using E_Learning.MediatorComponents.Queries;
using E_Learning.MiddleComponents.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Learning.Constants;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateCourse([FromBody] AddCourse addCourse)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole != Roles.Teacher.ToString())
                return Unauthorized();

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            addCourse.AuthorId = int.Parse(userId);

            var result = await _mediator.Send(addCourse);

            return result ? Ok(true) : BadRequest(false);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<object>> CourseDetails(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return new UnauthorizedResult();
            }

            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var requestObject = new GetCourseDetailsQuery { UserRole = userRole!, CourseId = id, UserId = int.Parse(userId) };
            var result = await _mediator.Send(requestObject);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userId != null && userRole != null && userRole == Roles.Student.ToString())
            {
                var studentRequestObject = new StudentCourseList() { Id = userId, Role = userRole };
                var studentCourses = await _mediator.Send(studentRequestObject);
                return Ok(studentCourses);
            }

            var deafultRequestObject = new DefaultCourseList() { Id = userId, Role = userRole };
            var defaultResult = await _mediator.Send(deafultRequestObject);
            return Ok(defaultResult);
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCourseCommand = new DeleteCourse(id);
            var result = await _mediator.Send(deleteCourseCommand);

            return result ? Ok() : NotFound();
        }
    }
}