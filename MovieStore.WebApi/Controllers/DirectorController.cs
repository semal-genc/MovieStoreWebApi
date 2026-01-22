using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Commands.Director.CreateDirector;
using MovieStore.Application.Commands.Director.DeleteDirector;
using MovieStore.Application.Commands.Director.UpdateDirector;
using MovieStore.Application.DTOs;
using MovieStore.Application.Queries.Director.GetDirectorById;
using MovieStore.Application.Queries.Director.GetDirectorList;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DirectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DirectorDto>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetDirectorListQuery();
            var director = await _mediator.Send(query, cancellationToken);
            return Ok(director);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DirectorDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetDirectorByIdQuery(id);
            var director = await _mediator.Send(query, cancellationToken);
            return Ok(director);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDirectorCommand command, CancellationToken cancellationToken)
        {
            var directorId = await _mediator.Send(command, cancellationToken);
            return Ok(directorId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteDirectorCommand(id);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] UpdateDirectorCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var updateDirectorId = await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Yönetmen başarıyla güncellendi.", DirectorId = updateDirectorId});
        }
    }
}