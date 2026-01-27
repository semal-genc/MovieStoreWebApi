using FluentValidation;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;

namespace MovieStore.Application.Features.Customers.Validators.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(2);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
