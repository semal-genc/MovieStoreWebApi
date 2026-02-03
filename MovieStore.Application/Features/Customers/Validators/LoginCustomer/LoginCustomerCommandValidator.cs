using FluentValidation;
using MovieStore.Application.Features.Customers.Commands.LoginCustomer;

namespace MovieStore.Application.Features.Customers.Validators.LoginCustomer
{
    public class LoginCustomerCommandValidator : AbstractValidator<LoginCustomerCommand>
    {
        public LoginCustomerCommandValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
