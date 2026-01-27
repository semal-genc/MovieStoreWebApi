using MediatR;

namespace MovieStore.Application.Features.Customers.Commands.LoginCustomer
{
    public class LoginCustomerCommand : IRequest<string>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}