using MediatR;

namespace MovieStore.Application.Commands.Actor.CreateActor
{
    public class CreateActorCommand : IRequest<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
