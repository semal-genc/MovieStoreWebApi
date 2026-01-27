using MediatR;

namespace MovieStore.Application.Features.Directors.Commands.DeleteDirector
{
    public record DeleteDirectorCommand(int Id) : IRequest;
}
