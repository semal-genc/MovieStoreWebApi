using MediatR;

namespace MovieStore.Application.Commands.Actor.DeleteActor
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
