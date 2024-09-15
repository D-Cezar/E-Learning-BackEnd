using E_Learning.MediatorComponents.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/[controller]/{courseId}")]
        public async Task<ActionResult> DashboardInfo([FromRoute] int courseId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var response = await _mediator.Send(new DashboardQuery() { CourseId = courseId, Role = userRole });

            if (response == null)
                return BadRequest();

            return Ok(response);
        }
    }
}