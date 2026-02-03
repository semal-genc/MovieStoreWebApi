using AutoMapper;
using FluentAssertions;
using MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Customers.Commands.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public AddFavoriteGenreCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_FavoriteGenre_ShouldBeAdded()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                PasswordHash = "hash"
            };
            
            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Customers.Add(customer); _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new AddFavoriteGenreCommand(customer.Id, genre.Id);
            var handler = new AddFavoriteGenreCommandHandler(_context, _mapper);

            await handler.Handle(command, CancellationToken.None);

            var favorite = _context.CustomerFavoriteGenres
                .SingleOrDefault(x => x.CustomerId == customer.Id && x.GenreId == genre.Id);

            favorite.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenCustomerDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new AddFavoriteGenreCommand(999, genre.Id);
            var handler = new AddFavoriteGenreCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Alıcı bulunamadı.");
        }

        [Fact]
        public async Task WhenGenreDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                PasswordHash = "hash"
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new AddFavoriteGenreCommand(customer.Id, 999);
            var handler = new AddFavoriteGenreCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Tür bulunamadı.");
        }

        [Fact]
        public async Task WhenFavoriteGenreAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.com",
                PasswordHash = "hash"
            };

            var genre = new Genre
            {
                Name = "Action"
            };

            _context.Customers.Add(customer);
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync(CancellationToken.None);

            _context.CustomerFavoriteGenres.Add(
                new CustomerFavoriteGenre
                {
                    CustomerId = customer.Id,
                    GenreId = genre.Id
                });

            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new AddFavoriteGenreCommand(customer.Id, genre.Id);
            var handler = new AddFavoriteGenreCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu tür zaten favorilere eklenmiş.");
        }
    }
}