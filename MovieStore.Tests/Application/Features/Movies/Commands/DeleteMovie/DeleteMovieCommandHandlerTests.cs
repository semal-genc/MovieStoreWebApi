using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Commands.DeleteMovie;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteMovieCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task WhenValidMovieIdIsGiven_Movie_ShouldBeDeactivated()
        {
            var movie = new Movie
            {
                Name = "Test Movie",
                Year = 2020,
                Price = 50,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteMovieCommand(movie.Id);
            var handler = new DeleteMovieCommandHandler(_context);

            await handler.Handle(command, CancellationToken.None);

            var deletedMovie = _context.Movies
                .IgnoreQueryFilters()
                .SingleOrDefault(x => x.Id == movie.Id);

            deletedMovie.Should().NotBeNull();
            deletedMovie!.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task WhenMovieDoesNotExist_KeyNotFoundException_ShouldBeThrown()
        {
            var command = new DeleteMovieCommand(999);
            var handler = new DeleteMovieCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Silinecek film bulunamadı.");
        }

        [Fact]
        public async Task WhenMovieIsAlreadyInactive_KeyNotFoundException_ShouldBeThrown()
        {
            var movie = new Movie
            {
                Name = "Inactive Movie",
                IsActive = false
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteMovieCommand(movie.Id);
            var handler = new DeleteMovieCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Silinecek film bulunamadı.");
        }
    }
}