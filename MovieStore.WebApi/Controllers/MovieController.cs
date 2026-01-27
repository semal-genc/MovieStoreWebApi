using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Features.Movies.Commands.CreateMovie;
using MovieStore.Application.Features.Movies.Commands.DeleteMovie;
using MovieStore.Application.Features.Movies.Commands.RestoreMovie;
using MovieStore.Application.Features.Movies.Commands.UpdateMovie;
using MovieStore.Application.Features.Movies.Dtos;
using MovieStore.Application.Features.Movies.Queries.GetInactiveMovies;
using MovieStore.Application.Features.Movies.Queries.GetMovieById;
using MovieStore.Application.Features.Movies.Queries.GetMovieList;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
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

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetMovieListQuery();
            var movies = await _mediator.Send(query, cancellationToken);
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetMovieByIdQuery(id);
            var movie = await _mediator.Send(query, cancellationToken);
            return Ok(movie);
        }

        [HttpGet("inactive")]
        public async Task<ActionResult<List<MovieDto>>> GetInactive(CancellationToken cancellationToken)
        {
            var query = new GetInactiveMoviesQuery();
            var movies = await _mediator.Send(query, cancellationToken);
            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteMovieCommand(id);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] UpdateMovieCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var updateMovieId = await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Film başarıyla güncellendi.", MovieId = updateMovieId });
        }

        [HttpPatch("{id}/restore")]
        public async Task<ActionResult<int>> Restore(int id, CancellationToken cancellationToken)
        {
            var command = new RestoreMovieCommand(id);
            var restoreMovieId = await _mediator.Send(command, cancellationToken);

            return Ok(new { restoreMovieId, message = "Film başarıyla geri getirildi." });
        }
    }
}