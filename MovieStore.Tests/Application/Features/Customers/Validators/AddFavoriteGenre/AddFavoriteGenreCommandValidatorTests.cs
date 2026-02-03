using FluentValidation.TestHelper;
using MovieStore.Application.Features.Customers.Commands.AddFavoriteGenre;
using MovieStore.Application.Features.Customers.Validators.AddFavoriteGenre;

namespace MovieStore.Tests.Application.Features.Customers.Validators.AddFavoriteGenre
{
    public class AddFavoriteGenreCommandValidatorTests
    {
        private readonly AddFavoriteGenreCommandValidator _validator;

        public AddFavoriteGenreCommandValidatorTests()
        {
            _validator = new AddFavoriteGenreCommandValidator();
        }

        [Fact]
        public void WhenValidInputsAreGiven_ValidatorShouldNotReturnError()
        {
            var command = new AddFavoriteGenreCommand(1, 2);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenCustomerIdIsInvalid_ValidatorShouldReturnError(int customerId)
        {
            var command = new AddFavoriteGenreCommand(customerId, 1);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.CustomerId)
                .WithErrorMessage("Geçerli bir CustomerId girilmelidir.");
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenGenreIdIsInvalid_ValidatorShouldReturnError(int genreId)
        {
            var command = new AddFavoriteGenreCommand(1, genreId);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.GenreId)
                .WithErrorMessage("Geçerli bir GenreId girilmelidir.");
        }
    }
}