using MediatR;

namespace MovieStore.Application.Features.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommand : IRequest<int>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
    }
}