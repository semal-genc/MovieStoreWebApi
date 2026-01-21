using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Commands.Movie.CreateMovie;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieCommand command, CancellationToken cancellationToken)
        {
            var movieId = await _mediator.Send(command, cancellationToken);
            return Ok(movieId);
        }
    }
}