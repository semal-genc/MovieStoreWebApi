using MediatR;

namespace MovieStore.Application.Features.Directors.Commands.UpdateDirector
{
    public class UpdateDirectorCommand : IRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
