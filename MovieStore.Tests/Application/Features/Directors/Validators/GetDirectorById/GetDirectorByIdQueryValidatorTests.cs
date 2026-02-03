using FluentValidation.TestHelper;
using MovieStore.Application.Features.Directors.Queries.GetDirectorById;
using MovieStore.Application.Features.Directors.Validators.GetDirectorById;

namespace MovieStore.Tests.Application.Features.Directors.Validators.GetDirectorById
{
    public class GetDirectorByIdQueryValidatorTests
    {
        private readonly GetDirectorByIdQueryValidator _validator;

        public GetDirectorByIdQueryValidatorTests()
        {
            _validator = new GetDirectorByIdQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenDirectorIdIsInvalid_ValidatorShouldReturnError(int directorId)
        {
            var query = new GetDirectorByIdQuery(directorId);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Yönetmen id 0'dan büyük olmalıdır.");
        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_ValidatorShouldNotReturnError()
        {
            var query = new GetDirectorByIdQuery(1);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}