using FluentValidation.TestHelper;
using MovieStore.Application.Features.Customers.Commands.CreateCustomer;
using MovieStore.Application.Features.Customers.Validators.CreateCustomer;

namespace MovieStore.Tests.Application.Features.Customers.Validators.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests
    {
        private readonly CreateCustomerCommandValidator _validator;

        public CreateCustomerCommandValidatorTests()
        {
            _validator = new CreateCustomerCommandValidator();
        }

        [Fact]
        public void WhenCommandIsValid_ValidatorShouldNotReturnError()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Şemal",
                LastName = "Genç",
                Email = "semal@test.com",
                Password = "123456"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "Ad alanı boş bırakılamaz.")]
        [InlineData("A", "Ad en az 2 karakter olmalıdır.")]
        public void WhenFirstNameIsInvalid_ValidatorShouldReturnError(string firstName, string expectedMessage)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = firstName,
                LastName = "Genç",
                Email = "test@test.com",
                Password = "123456"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "Soyad alanı boş bırakılamaz.")]
        [InlineData("G", "Soyad en az 2 karakter olmalıdır.")]
        public void WhenLastNameIsInvalid_ValidatorShouldReturnError(string lastName, string expectedMessage)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Şemal",
                LastName = lastName,
                Email = "test@test.com",
                Password = "123456"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "E-posta adresi boş bırakılamaz.")]
        [InlineData("invalid-email", "Geçerli bir e-posta adresi giriniz.")]
        public void WhenEmailIsInvalid_ValidatorShouldReturnError(string email, string expectedMessage)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Şemal",
                LastName = "Genç",
                Email = email,
                Password = "123456"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "Şifre alanı boş bırakılamaz.")]
        [InlineData("123", "Şifre en az 6 karakter olmalıdır.")]
        public void WhenPasswordIsInvalid_ValidatorShouldReturnError(string password, string expectedMessage)
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "Şemal",
                LastName = "Genç",
                Email = "test@test.com",
                Password = password
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password)
                .WithErrorMessage(expectedMessage);
        }
    }
}