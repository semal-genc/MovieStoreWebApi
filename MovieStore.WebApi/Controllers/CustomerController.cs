using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;
using MovieStore.Application.Features.Customers.Commands.DeleteCustomer;
using MovieStore.Application.Features.Customers.Commands.LoginCustomer;
using MovieStore.Application.Features.Customers.Queries.GetFavoriteGenres;
using MovieStore.Application.Features.Orders.Commands.BuyMovie;

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
            return Ok(new { Token = token });
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

        [Authorize]
        [HttpPost("buy")]
        public async Task<IActionResult> BuyMovie([FromBody] BuyMovieCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            request.CustomerId = int.Parse(userIdClaim.Value);

            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavoriteGenres()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            var customerId = int.Parse(userIdClaim.Value);

            var query = new GetFavoriteGenresQuery(customerId);
            var favoriteGenres = await _mediator.Send(query);

            return Ok(favoriteGenres);
        }

        [Authorize]
        [HttpPost("favorites")]
        public async Task<IActionResult> AddFavoriteGenre([FromBody] AddFavoriteGenreCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            request.CustomerId = int.Parse(userIdClaim.Value);

            await _mediator.Send(request, cancellationToken);
            return Ok(new { Message = "Favori tür eklendi." });
        }
    }
}