using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Commands.CreateMovie;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenMovieAlreadyExists_InvalidOperationException_ShouldBeThrown()
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
            await _context.SaveChangesAsync(CancellationToken.None);

            _context.Movies.Add(new Movie
            {
                Name = "Inception",
                Year = 2010,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 100,
                IsActive = true
            });

            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateMovieCommand
            {
                Name = "Inception",
                Year = 2010,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 100
            };

            var handler = new CreateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu film zaten mevcut.");
        }

        [Fact]
        public async Task WhenDirectorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateMovieCommand
            {
                Name = "Snatch",
                Year = 2000,
                DirectorId = 999,
                GenreId = genre.Id,
                Price = 50
            };

            var handler = new CreateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Böyle bir yönetmen bulunamadı.");
        }

        [Fact]
        public async Task WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var director = new Director
            {
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            _context.Directors.Add(director);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateMovieCommand
            {
                Name = "Snatch",
                Year = 2000,
                DirectorId = director.Id,
                GenreId = 999,
                Price = 50
            };

            var handler = new CreateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Böyle bir tür bulunamadı.");
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Movie_ShouldBeCreated()
        {
            var director = new Director
            {
                FirstName = "Quentin",
                LastName = "Tarantino"
            };

            var genre = new Genre
            {
                Name = "Crime"
            };

            _context.Directors.Add(director);
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateMovieCommand
            {
                Name = "Pulp Fiction",
                Year = 1994,
                DirectorId = director.Id,
                GenreId = genre.Id,
                Price = 80
            };

            var handler = new CreateMovieCommandHandler(_context, _mapper);

            var movieId = await handler.Handle(command, CancellationToken.None);

            var movie = await _context.Movies.SingleOrDefaultAsync(x => x.Id == movieId);

            movie.Should().NotBeNull();
            movie.Name.Should().Be(command.Name);
            movie.Year.Should().Be(command.Year);
            movie.DirectorId.Should().Be(command.DirectorId);
            movie.GenreId.Should().Be(command.GenreId);
            movie.IsActive.Should().BeTrue();
        }
    }
}