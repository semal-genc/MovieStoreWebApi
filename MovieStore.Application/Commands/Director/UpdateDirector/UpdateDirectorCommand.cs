using MediatR;

namespace MovieStore.Application.Commands.Director.UpdateDirector
{
    public class UpdateDirectorCommand : IRequest
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
