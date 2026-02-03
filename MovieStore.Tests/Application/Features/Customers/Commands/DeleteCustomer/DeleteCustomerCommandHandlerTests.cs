using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Features.Customers.Commands.DeleteCustomer;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Domain.Entities;
using MovieStore.Tests.TestSetup;

namespace MovieStore.Tests.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandlerTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteCustomerCommandHandlerTests(CommonTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task WhenValidCustomerIdIsGiven_Customer_ShouldBeDeleted()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Customer",
                Email = "customer@test.com",
                PasswordHash = "hash"
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(CancellationToken.None);

            var command = new DeleteCustomerCommand(customer.Id);
            var handler = new DeleteCustomerCommandHandler(_context);

            await handler.Handle(command, CancellationToken.None);

            var deletedCustomer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == customer.Id);

            deletedCustomer.Should().BeNull();
        }

        [Fact]
        public async Task WhenCustomerDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var command = new DeleteCustomerCommand(999);
            var handler = new DeleteCustomerCommandHandler(_context);

            Func<Task> act = async () =>
                await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage("Customer bulunamadı.");
        }
    }
}