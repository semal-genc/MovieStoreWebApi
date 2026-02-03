using FluentValidation.TestHelper;
using MovieStore.Application.Features.Directors.Commands.DeleteDirector;
using MovieStore.Application.Features.Directors.Validators.DeleteDirector;

namespace MovieStore.Tests.Application.Features.Directors.Validators.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests
    {
        private readonly DeleteDirectorCommandValidator _validator;

        public DeleteDirectorCommandValidatorTests()
        {
            _validator = new DeleteDirectorCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenDirectorIdIsInvalid_ValidatorShouldReturnError(int directorId)
        {
            var command = new DeleteDirectorCommand(directorId);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Geçerli bir DirectorId girilmelidir.");
        }

        [Fact]
        public void WhenDirectorIdIsValid_ValidatorShouldNotReturnError()
        {
            var command = new DeleteDirectorCommand(1);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}