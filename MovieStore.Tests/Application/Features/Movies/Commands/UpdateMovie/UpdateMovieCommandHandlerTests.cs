using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Movies.Commands.UpdateMovie;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateMovieCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenMovieDoesNotExist_KeyNotFoundException_ShouldBeThrown()
        {
            var command = new UpdateMovieCommand
            {
                Id = 999,
                Name = "Updated",
                Year = 2022,
                Price = 50,
                DirectorId = 1,
                GenreId = 1
            };

            var handler = new UpdateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage("Böyle bir film bulunamadı.");
        }

        [Fact]
        public async Task WhenDirectorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var director = new Director
            {
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Directors.Add(director);
            _context.Genres.Add(genre);

            var movie = new Movie
            {
                Name = "Movie",
                Year = 2020,
                Price = 30,
                Genre = genre,
                Director = director,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateMovieCommand
            {
                Id = movie.Id,
                Name = "Updated",
                Year = 2022,
                Price = 50,
                DirectorId = 999,
                GenreId = genre.Id
            };

            var handler = new UpdateMovieCommandHandler(_context, _mapper);

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

            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Directors.Add(director);
            _context.Genres.Add(genre);

            var movie = new Movie
            {
                Name = "Movie",
                Year = 2020,
                Price = 30,
                Director = director,
                Genre = genre,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateMovieCommand
            {
                Id = movie.Id,
                Name = "Updated",
                Year = 2022,
                Price = 50,
                DirectorId = director.Id,
                GenreId = 999
            };

            var handler = new UpdateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Böyle bir tür bulunamadı.");
        }

        [Fact]
        public async Task WhenDuplicateMovieExists_InvalidOperationException_ShouldBeThrown()
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

            var existingMovie = new Movie
            {
                Name = "Inception",
                Year = 2010,
                Director = director,
                Genre = genre,
                IsActive = true
            };

            var movieToUpdate = new Movie
            {
                Name = "Old Name",
                Year = 2005,
                Director = director,
                Genre = genre,
                IsActive = true
            };

            _context.Movies.AddRange(existingMovie, movieToUpdate);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateMovieCommand
            {
                Id = movieToUpdate.Id,
                Name = "Inception",
                Year = 2010,
                DirectorId = director.Id,
                GenreId = genre.Id
            };

            var handler = new UpdateMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu film zaten mevcut.");
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Movie_ShouldBeUpdated_AndIdReturned()
        {
            var director = new Director
            {
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            var genre = new Genre
            {
                Name = "Action"
            };

            var movie = new Movie
            {
                Name = "Old Name",
                Year = 2010,
                Price = 20,
                Director = director,
                Genre = genre,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new UpdateMovieCommand
            {
                Id = movie.Id,
                Name = "New Name",
                Year = 2022,
                Price = 60,
                DirectorId = director.Id,
                GenreId = genre.Id
            };

            var handler = new UpdateMovieCommandHandler(_context, _mapper);

            var updatedMovieId = await handler.Handle(command, CancellationToken.None);

            var updatedMovie = await _context.Movies
                .SingleOrDefaultAsync(x => x.Id == updatedMovieId);

            updatedMovie.Should().NotBeNull();
            updatedMovie!.Name.Should().Be(command.Name);
            updatedMovie.Year.Should().Be(command.Year);
            updatedMovie.Price.Should().Be(command.Price);
        }
    }
}