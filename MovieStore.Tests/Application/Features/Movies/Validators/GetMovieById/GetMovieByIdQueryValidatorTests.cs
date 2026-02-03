using FluentValidation.TestHelper;
using MovieStore.Application.Features.Movies.Queries.GetMovieById;
using MovieStore.Application.Features.Movies.Validators.GetMovieById;

namespace MovieStore.Tests.Application.Features.Movies.Validators.GetMovieById
{
    public class GetMovieByIdQueryValidatorTests
    {
        private readonly GetMovieByIdQueryValidator _validator;

        public GetMovieByIdQueryValidatorTests()
        {
            _validator = new GetMovieByIdQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdIsInvalid_ValidationError_ShouldBeReturned(int id)
        {
            var query = new GetMovieByIdQuery(id);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Film Id değeri 0'dan büyük olmalıdır.");
        }

        [Fact]
        public void WhenIdIsGreaterThanZero_NoValidationError_ShouldBeReturned()
        {
            var query = new GetMovieByIdQuery(1);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}