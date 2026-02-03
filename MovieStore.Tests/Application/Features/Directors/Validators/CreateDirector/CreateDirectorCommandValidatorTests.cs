using FluentValidation.TestHelper;
using MovieStore.Application.Features.Directors.Commands.CreateDirector;
using MovieStore.Application.Features.Directors.Validators.CreateDirector;

namespace MovieStore.Tests.Application.Features.Directors.Validators.CreateDirector
{
    public class CreateDirectorCommandValidatorTests
    {
        private readonly CreateDirectorCommandValidator _validator;

        public CreateDirectorCommandValidatorTests()
        {
            _validator = new CreateDirectorCommandValidator();
        }

        [Theory]
        [InlineData("", "Yönetmen adı boş olamaz.")]
        [InlineData("G", "Yönetmen adı en az 2 karakter olmalıdır.")]
        public void WhenFirstNameIsInvalid_ValidationError_ShouldBeReturned(string firstName, string expectedMessage)
        {
            var command = new CreateDirectorCommand
            {
                FirstName = firstName,
                LastName = "Ritchie"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(expectedMessage);
        }
        
        [Theory]
        [InlineData("", "Yönetmen soyadı boş olamaz.")]
        [InlineData("R", "Yönetmen soyadı en az 2 karakter olmalıdır.")]
        public void WhenLastNameIsInvalid_ValidationError_ShouldBeReturned(string lastName, string expectedMessage)
        {
            var command = new CreateDirectorCommand
            {
                FirstName = "Guy",
                LastName = lastName
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(expectedMessage);
        }

        [Fact]
        public void WhenCommandIsValid_ValidationErrors_ShouldNotBeReturned()
        {
            var command = new CreateDirectorCommand
            {
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            var result=_validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}