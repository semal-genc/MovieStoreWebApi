
using FluentValidation.TestHelper;
using MovieStore.Application.Features.Actors.Queries.GetActorById;
using MovieStore.Application.Features.Actors.Validators.GetActorById;

namespace MovieStore.Tests.Application.Features.Actors.Validators.GetActorById
{
    public class GetActorByIdQueryValidatorTests
    {
        private readonly GetActorByIdQueryValidator _validator;

        public GetActorByIdQueryValidatorTests()
        {
            _validator = new GetActorByIdQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void WhenActorIdIsInvalid_ValidatorShouldReturnError(int id)
        {
            var query = new GetActorByIdQuery(id);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Oyuncu id 0'dan büyük olmalıdır.");
        }

        [Fact]
        public void WhenValidActorIdIsGiven_ValidatorShouldNotReturnError()
        {
            var query = new GetActorByIdQuery(1);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}