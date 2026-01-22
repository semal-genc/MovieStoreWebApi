using MediatR;

namespace MovieStore.Application.Commands.Actor.UpdateActor
{
    public class UpdateActorCommand : IRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}