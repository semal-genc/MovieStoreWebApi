using MediatR;

namespace MovieStore.Application.Features.Actors.Commands.CreateActor
{
    public class CreateActorCommand : IRequest<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
