using MediatR;

namespace MovieStore.Application.Features.Actors.Commands.DeleteActor
{
    public class DeleteActorCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteActorCommand(int id)
        {
            Id = id;
        }
    }
}
