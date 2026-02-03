using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Movies.Queries.GetInactiveMovies;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Queries.GetInactiveMovies
{
    public class GetInactiveMoviesQueryHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetInactiveMoviesQueryHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenInactiveMoviesExist_InactiveMovieDtoList_ShouldBeReturned()
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

            var activeMovie = new Movie
            {
                Name = "Active Movie",
                Year = 2020,
                Price = 40,
                IsActive = true,
                Director = director,
                Genre = genre
            };

            var inactiveMovie = new Movie
            {
                Name = "Inactive Movie",
                Year = 2018,
                Price = 30,
                IsActive = false,
                Director = director,
                Genre = genre
            };

            _context.Movies.AddRange(activeMovie, inactiveMovie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetInactiveMoviesQuery();
            var handler = new GetInactiveMoviesQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            result[0].Name.Should().Be(inactiveMovie.Name);
            result[0].GenreName.Should().Be(inactiveMovie.Genre.Name);
            result[0].DirectorFullName.Should().Be($"{inactiveMovie.Director.FirstName} {inactiveMovie.Director.LastName}");
        }

        [Fact]
        public async Task WhenNoInactiveMoviesExist_EmptyList_ShouldBeReturned()
        {
            var movie = new Movie
            {
                Name = "Only Active Movie",
                Year = 2021,
                Price = 50,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var query = new GetInactiveMoviesQuery();
            var handler = new GetInactiveMoviesQueryHandler(_context, _mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}