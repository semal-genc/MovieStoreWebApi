using System.Security.Cryptography;
using System.Text;
using MediatR;
using MovieStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.Interfaces.Persistence;
using MovieStore.Application.Interfaces.Security;

namespace MovieStore.Application.Features.Customers.Commands.LoginCustomer
{
    public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, string>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCustomerCommandHandler(IMovieStoreDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (customer is null)
                throw new InvalidOperationException("Email veya şifre hatalı.");

            var hasher = new PasswordHasher<Customer>();

            var result=hasher.VerifyHashedPassword(
                customer,
                customer.PasswordHash,
                request.Password
            );

            if (result != PasswordVerificationResult.Success)
                throw new InvalidOperationException("Email veya şifre hatalı.");

            return _jwtTokenService.CreateToken(customer);
        }

        private static string ComputeHash(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}