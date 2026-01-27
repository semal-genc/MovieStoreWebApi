using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Commands.Customer.CreateCustomer;
using MovieStore.Application.Commands.Customer.DeleteCustomer;
using MovieStore.Application.Commands.Customer.LoginCustomer;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerId = await _mediator.Send(request, cancellationToken);
            return Ok(customerId);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerCommand request, CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(request, cancellationToken);
            return Ok(new { token });
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            var customerId = int.Parse(userIdClaim.Value);

            var command = new DeleteCustomerCommand(customerId);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}