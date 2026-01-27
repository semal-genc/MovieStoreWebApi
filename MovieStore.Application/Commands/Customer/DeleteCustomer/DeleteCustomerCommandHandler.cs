using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;

namespace MovieStore.Application.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteCustomerCommandHandler(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

            if (customer is null)
                throw new InvalidOperationException("Customer bulunamadı.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}