using MediatR;

namespace MovieStore.Application.Commands.Director.CreateDirector
{
    public class CreateDirectorCommand : IRequest<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
