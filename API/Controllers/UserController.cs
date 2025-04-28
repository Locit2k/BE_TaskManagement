using Application.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var userName = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;
            var command = new GetUserCommand()
            {
                UserName = userName
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
