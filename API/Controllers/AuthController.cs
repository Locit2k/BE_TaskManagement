using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Test API");
        }
    }
}
