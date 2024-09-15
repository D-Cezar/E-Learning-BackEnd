using E_Learning.DB.Models.AdminComponentsDTOs;
using E_Learning.DTOs.Responses;
using E_Learning.MediatorComponents.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Learning.Constants;

namespace E_Learning.Controllers
{
    [Controller]
    [Authorize]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/UsersInfo/{courseId}")]
        public async Task<ActionResult<List<AdminUserContentDTO>>> GetActiveUsers([FromRoute] int courseId)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (userRole != Roles.Teacher.ToString())
                return Unauthorized();

            var request = new ActiveUsersQuery() { CourseId = courseId };

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}