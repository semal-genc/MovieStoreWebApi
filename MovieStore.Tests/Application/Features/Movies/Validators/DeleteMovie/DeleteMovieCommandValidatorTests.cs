
using FluentValidation.TestHelper;
using MovieStore.Application.Features.Movies.Commands.DeleteMovie;
using MovieStore.Application.Features.Movies.Validators.DeleteMovie;

namespace MovieStore.Tests.Application.Features.Movies.Validators.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests
    {
        private readonly DeleteMovieCommandValidator _validator;

        public DeleteMovieCommandValidatorTests()
        {
            _validator = new DeleteMovieCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdIsInvalid_ValidationError_ShouldBeReturned(int id)
        {
            var command = new DeleteMovieCommand(id);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Geçerli bir film Id'si girilmelidir.");
        }

        [Fact]
        public void WhenIdIsGreaterThanZero_NoValidationError_ShouldBeReturned()
        {
            var command = new DeleteMovieCommand(1);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}