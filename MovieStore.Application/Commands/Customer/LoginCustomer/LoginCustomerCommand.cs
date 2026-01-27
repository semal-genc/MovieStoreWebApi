using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Commands.Customer.LoginCustomer
{
    public class LoginCustomerCommand : IRequest<string>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}