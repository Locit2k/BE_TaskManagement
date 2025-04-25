using Application.DTOs;
using Application.Features.Employee.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
