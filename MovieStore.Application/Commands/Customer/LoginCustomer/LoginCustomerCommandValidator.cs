using FluentValidation;

namespace MovieStore.Application.Commands.Customer.LoginCustomer
{
    public class LoginCustomerCommandValidator : AbstractValidator<LoginCustomerCommand>
    {
        public LoginCustomerCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
