using FluentValidation.TestHelper;
using MovieStore.Application.Features.Orders.Commands.BuyMovie;
using MovieStore.Application.Features.Orders.Validators.BuyMovie;

namespace MovieStore.Tests.Application.Features.Orders.Validators.BuyMovie
{
    public class BuyMovieCommandValidatorTests
    {
         private readonly BuyMovieCommandValidator _validator;

        public BuyMovieCommandValidatorTests()
        {
            _validator = new BuyMovieCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenMovieIdIsLessThanOrEqualToZero_Validation_ShouldHaveError(int movieId)
        {
            var command = new BuyMovieCommand
            {
                MovieId = movieId,
                CustomerId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.MovieId);
        }

        [Fact]
        public void WhenMovieIdIsGreaterThanZero_Validation_ShouldNotHaveError()
        {
            var command = new BuyMovieCommand
            {
                MovieId = 1,
                CustomerId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.MovieId);
        }
    }
}