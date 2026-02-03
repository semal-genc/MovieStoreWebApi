using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using MovieStore.Application.Features.Customers.Commands.LoginCustomer;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Application.Interfaces.Security;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Customers.Commands.LoginCustomer
{
    public class LoginCustomerCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;

        public LoginCustomerCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        }

        [Fact]
        public async Task WhenEmailDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new LoginCustomerCommand
            {
                Email = "notfound@test.com",
                Password = "123456"
            };

            var handler = new LoginCustomerCommandHandler(_context, _jwtTokenServiceMock.Object);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Email veya şifre hatalı.");
        }

        [Fact]
        public async Task WhenPasswordIsWrong_InvalidOperationException_ShouldBeThrown()
        {
            var customer = new Customer
            {
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };

            var hasher = new PasswordHasher<Customer>();
            customer.PasswordHash = hasher.HashPassword(customer, "correctPassword");

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new LoginCustomerCommand
            {
                Email = "test@test.com",
                Password = "wrongPassword"
            };

            var handler = new LoginCustomerCommandHandler(_context, _jwtTokenServiceMock.Object);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Email veya şifre hatalı.");
        }

        [Fact]
        public async Task WhenValidCredentialsAreGiven_Token_ShouldBeReturned()
        {
            var customer = new Customer
            {
                Email = "valid@test.com",
                FirstName = "Valid",
                LastName = "User"
            };

            var hasher = new PasswordHasher<Customer>();
            customer.PasswordHash = hasher.HashPassword(customer, "123456");

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            _jwtTokenServiceMock
                .Setup(x => x.CreateToken(It.IsAny<Customer>()))
                .Returns("fake-jwt-token");

            var command = new LoginCustomerCommand
            {
                Email = "valid@test.com",
                Password = "123456"
            };

            var handler = new LoginCustomerCommandHandler(_context, _jwtTokenServiceMock.Object);

            var token = await handler.Handle(command, CancellationToken.None);

            token.Should().Be("fake-jwt-token");
        }
    }
}