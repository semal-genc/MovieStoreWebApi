
using FluentValidation.TestHelper;
using MovieStore.Application.Features.Customers.Commands.DeleteCustomer;
using MovieStore.Application.Features.Customers.Validators.DeleteCustomer;

namespace MovieStore.Tests.Application.Features.Customers.Validators.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests
    {
        private readonly DeleteCustomerCommandValidator _validator;

        public DeleteCustomerCommandValidatorTests()
        {
            _validator = new DeleteCustomerCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenCustomerIdIsInvalid_ValidatorShouldReturnError(int customerId)
        {
            var command = new DeleteCustomerCommand(customerId);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.CustomerId)
                .WithErrorMessage("Geçerli bir CustomerId girilmelidir.");
        }

        [Fact]
        public void WhenCustomerIdIsValid_ValidatorShouldNotReturnError()
        {
            var command = new DeleteCustomerCommand(1);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}