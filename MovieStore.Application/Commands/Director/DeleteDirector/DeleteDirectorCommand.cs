using MediatR;

namespace MovieStore.Application.Commands.Director.DeleteDirector
{
    public record DeleteDirectorCommand(int Id) : IRequest;
}
