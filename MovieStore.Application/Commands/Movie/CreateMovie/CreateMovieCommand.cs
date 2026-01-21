using MediatR;

namespace MovieStore.Application.Commands.Movie.CreateMovie
{
    public class CreateMovieCommand : IRequest<int>
    {
        public required string Name { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}