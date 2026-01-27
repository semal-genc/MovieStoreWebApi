using FluentValidation;
using MovieStore.Application.Features.Customers.Commands.DeleteCustomer;

namespace MovieStore.Application.Features.Customers.Validators.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);
        }
    }
}