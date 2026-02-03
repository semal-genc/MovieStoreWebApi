using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Orders.Commands.BuyMovie;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Orders.Commands.BuyMovie
{
    public class BuyMovieCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public BuyMovieCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        private BuyMovieCommand CreateValidCommand()
        {
            return new BuyMovieCommand
            {
                MovieId = 1,
                CustomerId = 1
            };
        }

        [Fact]
        public async Task WhenMovieDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = CreateValidCommand();

            var handler = new BuyMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Film bulunamadı.");
        }

        [Fact]
        public async Task WhenCustomerDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var movie = new Movie
            {
                Name = "Movie",
                Year = 2020,
                Price = 50,
                IsActive = true
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = CreateValidCommand();
            command.MovieId = movie.Id;
            command.CustomerId = 999;

            var handler = new BuyMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Müşteri bulunamadı.");
        }

        [Fact]
        public async Task WhenMovieAlreadyPurchased_InvalidOperationException_ShouldBeThrown()
        {
            var movie = new Movie
            {
                Name = "Movie",
                Year = 2020,
                Price = 50,
                IsActive = true
            };

            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                PasswordHash = "123456"
            };

            var order = new Order
            {
                Movie = movie,
                Customer = customer,
                Price = 50,
                PurchaseDate = DateTime.UtcNow
            };

            _context.Movies.Add(movie);
            _context.Customers.Add(customer);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(CancellationToken.None);

            var commad = CreateValidCommand();
            commad.MovieId = movie.Id;
            commad.CustomerId = customer.Id;

            var handler = new BuyMovieCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(commad, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu film daha önce satın alınmış.");
        }

        [Fact]
        public async Task WhenInputsAreValid_Order_ShouldBeCreated()
        {
            var movie = new Movie
            {
                Name = "Movie",
                Year = 2020,
                Price = 50,
                IsActive = true
            };

            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@doe.com",
                PasswordHash = "123456"
            };

            _context.Movies.Add(movie);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = CreateValidCommand();
            command.MovieId = movie.Id;
            command.CustomerId = customer.Id;

            var handler = new BuyMovieCommandHandler(_context, _mapper);

            await handler.Handle(command, CancellationToken.None);

            var order = await _context.Orders.FirstOrDefaultAsync();

            order.Should().NotBeNull();
            order!.MovieId.Should().Be(movie.Id);
            order.CustomerId.Should().Be(customer.Id);
            order.Price.Should().Be(movie.Price);
        }
    }
}
