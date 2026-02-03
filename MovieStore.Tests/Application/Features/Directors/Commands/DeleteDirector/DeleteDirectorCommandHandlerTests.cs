using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Directors.Commands.DeleteDirector;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Directors.Commands.DeleteDirector
{
    public class DeleteDirectorCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteDirectorCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task WhenDirectorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new DeleteDirectorCommand(999);

            var handler = new DeleteDirectorCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Yönetmen bulunamadı.");
        }

        [Fact]
        public async Task WhenDirectorHasMovies_InvalidOperationException_ShouldBeThrown()
        {
            var director = new Director
            {
                FirstName = "James",
                LastName = "Cameron",
                Movies = new List<Movie>
                {
                    new Movie
                    {
                        Name = "Avatar",
                        Year = 2009,
                        Price = 100,
                        IsActive = true
                    }
                }
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteDirectorCommand(director.Id);

            var handler = new DeleteDirectorCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu yönetmenin filmleri bulunmaktadır. Silme işlemi yapılamaz.");
        }

        [Fact]
        public async Task WhenDirectorHasNoMovies_Director_ShouldBeDeleted()
        {
            var director = new Director
            {
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteDirectorCommand(director.Id);

            var handler = new DeleteDirectorCommandHandler(_context);

            await handler.Handle(command, CancellationToken.None);

            var deletedDirector = await _context.Directors
                .SingleOrDefaultAsync(x => x.Id == director.Id);
            deletedDirector.Should().BeNull();
        }
    }
}