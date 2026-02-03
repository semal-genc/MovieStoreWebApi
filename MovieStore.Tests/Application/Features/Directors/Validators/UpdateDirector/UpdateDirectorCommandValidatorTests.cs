using FluentValidation.TestHelper;
using MovieStore.Application.Features.Directors.Commands.UpdateDirector;
using MovieStore.Application.Features.Directors.Validators.UpdateDirector;

namespace MovieStore.Tests.Application.Features.Directors.Validators.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests
    {
        private readonly UpdateDirectorCommandValidator _validator;

        public UpdateDirectorCommandValidatorTests()
        {
            _validator = new UpdateDirectorCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenDirectorIdIsInvalid_ValidationError_ShouldBeReturned(int directorId)
        {
            var command = new UpdateDirectorCommand
            {
                Id = directorId,
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Yönetmen Id sıfırdan büyük olmalıdır.");
        }

        [Theory]
        [InlineData("", "Yönetmenin adı boş olamaz.")]
        [InlineData("G", "Yönetmen adı en az 2 karakter olmalıdır.")]
        public void WhenFirstNameIsInvalid_ValidationError_ShouldBeReturned(string firstName, string expectedMessage)
        {
            var command = new UpdateDirectorCommand
            {
                Id = 1,
                FirstName = firstName,
                LastName = "Ritchie"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "Yönetmenin soyadı boş olamaz.")]
        [InlineData("R", "Yönetmen soyadı en az 2 karakter olmalıdır.")]
        public void WhenLastNameIsInvalid_ValidationError_ShouldBeReturned(string lastName, string expectedMessage)
        {
            var command = new UpdateDirectorCommand
            {
                Id = 1,
                FirstName = "Guy",
                LastName = lastName
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(expectedMessage);
        }

        [Fact]
        public void WhenCommandIsValid_NoValidationError_ShouldBeReturned()
        {
            var command = new UpdateDirectorCommand
            {
                Id = 1,
                FirstName = "Guy",
                LastName = "Ritchie"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}