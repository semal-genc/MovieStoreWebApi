using MediatR;

namespace MovieStore.Application.Features.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteMovieCommand(int id)
        {
            Id = id;
        }
    }
}