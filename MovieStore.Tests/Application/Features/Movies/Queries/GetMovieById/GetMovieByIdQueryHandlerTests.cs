using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Movies.Queries.GetMovieById;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieByIdQueryHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenMovieDoesNotExist_KeyNotFoundException_ShouldBeThrown()
        {
            var query = new GetMovieByIdQuery(999);

            var handler = new GetMovieByIdQueryHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(query, CancellationToken.None);

            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Böyle bir film bulunamadı.");
        }

        [Fact]
        public async Task WhenMovieExists_MovieDto_ShouldBeReturned()
        {
            var director = new Director
            {
                FirstName = "Christopher",
                LastName = "Nolan"
            };

            var genre = new Genre
            {
                Name = "Sci-Fi"
            };

            _context.Directors.Add(director);
            _context.Genres.Add(genre);

            var movie = new Movie
            {
                Name = "Inception",
                Year = 2010,
                Price = 50,
                Director = director,
                Genre = genre,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetMovieByIdQuery(movie.Id);
            var handler = new GetMovieByIdQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(movie.Id);
            result.Name.Should().Be(movie.Name);
            result.Year.Should().Be(movie.Year);
            result.Price.Should().Be(movie.Price);
            result.GenreName.Should().Be(genre.Name);
            result.DirectorFullName.Should().Be($"{director.FirstName} {director.LastName}");
        }
    }
}