using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.Commands.Actor.CreateActor;
using MovieStore.Application.Commands.Actor.DeleteActor;
using MovieStore.Application.Commands.Actor.UpdateActor;
using MovieStore.Application.DTOs;
using MovieStore.Application.Queries.Actor.GetActorById;
using MovieStore.Application.Queries.Actor.GetActorList;

namespace MovieStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ActorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDto>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetActorListQuery();
            var actors = await _mediator.Send(query, cancellationToken);
            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDetailDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetActorByIdQuery(id);
            var actors = await _mediator.Send(query, cancellationToken);
            return Ok(actors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActorCommand command, CancellationToken cancellationToken)
        {
            var actorId = await _mediator.Send(command, cancellationToken);
            return Ok(actorId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteActorCommand(id);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] UpdateActorCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            var updateActorId = await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Aktör başarıyla güncellendi.", MovieId = updateActorId });
        }
    }
}