using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces;
using CustomerEntity = MovieStore.Domain.Entities.Customer;

namespace MovieStore.Application.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _context.Customers
                .AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (emailExists)
                throw new InvalidOperationException("Bu email zaten kullanılıyor.");

            var customer = _mapper.Map<CustomerEntity>(request);

            var hasher = new PasswordHasher<CustomerEntity>();
            customer.PasswordHash = hasher.HashPassword(customer, request.Password);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}