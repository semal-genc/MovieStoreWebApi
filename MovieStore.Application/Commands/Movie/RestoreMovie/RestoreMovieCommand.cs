using MediatR;

namespace MovieStore.Application.Commands.Movie.RestoreMovie
{
    public class RestoreMovieCommand : IRequest<int>
    {
        public int Id { get; set; }

        public RestoreMovieCommand(int id)
        {
            Id = id;
        }
    }
}