using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task WhenValidInputsAreGiven_Customer_ShouldBeCreated()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Test",
                LastName = "User",
                Email = "user@test.com",
                Password = "123456"
            };

            var handler = new CreateCustomerCommandHandler(_context, _mapper);

            var customerId = await handler.Handle(command, CancellationToken.None);

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            customer.Should().NotBeNull();
            customer.FirstName.Should().Be(command.FirstName);
            customer.LastName.Should().Be(command.LastName);
            customer.Email.Should().Be(command.Email);

            customer.PasswordHash.Should().NotBeNullOrEmpty();
            customer.PasswordHash.Should().NotBe(command.Password);
        }

        [Fact]
        public async Task WhenEmailAlreadyExists_InvalidOperationException_ShouldBeThrown()
        {
            _context.Customers.Add(new Customer
            {
                FirstName = "Existing",
                LastName = "User",
                Email = "duplicate@test.com",
                PasswordHash = "hash"
            });
            
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new CreateCustomerCommand
            {
                FirstName = "New",
                LastName = "User",
                Email = "duplicate@test.com",
                Password = "123456"
            };

            var handler = new CreateCustomerCommandHandler(_context, _mapper);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Bu email zaten kullanılıyor.");
        }
    }
}