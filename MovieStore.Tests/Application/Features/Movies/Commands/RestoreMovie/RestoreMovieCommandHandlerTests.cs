using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Commands.RestoreMovie;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Commands.RestoreMovie
{
    public class RestoreMovieCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public RestoreMovieCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task WhenMovieDoesNotExist_KeyNotFoundException_ShouldBeThrown()
        {
            var command=new RestoreMovieCommand(999);
            var handler=new RestoreMovieCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Böyle bir film bulunamadı.");
        }

        [Fact]
        public async Task WhenMovieIsAlreadyActive_InvalidOperationException_ShouldBeThrown()
        {
            var movie = new Movie
            {
                Name = "Active Movie",
                Year = 2022,
                Price = 40,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new RestoreMovieCommand(movie.Id);
            var handler = new RestoreMovieCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Film zaten aktif durumda.");
        }

        [Fact]
        public async Task WhenMovieIsInactive_Movie_ShouldBeRestored()
        {
            var movie = new Movie
            {
                Name = "Deleted Movie",
                Year = 2020,
                Price = 30,
                IsActive = false
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new RestoreMovieCommand(movie.Id);
            var handler = new RestoreMovieCommandHandler(_context);

            var restoredMovieId = await handler.Handle(command, CancellationToken.None);

            restoredMovieId.Should().Be(movie.Id);

            var restoredMovie = _context.Movies
                .IgnoreQueryFilters()
                .SingleOrDefault(x => x.Id == movie.Id);

            restoredMovie.Should().NotBeNull();
            restoredMovie!.IsActive.Should().BeTrue();
        }
    }
}