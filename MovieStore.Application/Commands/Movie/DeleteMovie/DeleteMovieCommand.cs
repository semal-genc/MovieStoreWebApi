using MediatR;

namespace MovieStore.Application.Commands.Movie.DeleteMovie
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