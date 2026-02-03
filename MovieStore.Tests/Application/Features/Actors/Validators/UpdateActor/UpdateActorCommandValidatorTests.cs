using FluentValidation.TestHelper;
using MovieStore.Application.Features.Actors.Commands.UpdateActor;
using MovieStore.Application.Features.Actors.Validators.UpdateActor;

namespace MovieStore.Tests.Application.Features.Actors.Validators.UpdateActor
{
    public class UpdateActorCommandValidatorTests
    {
        private readonly UpdateActorCommandValidator _validator;

        public UpdateActorCommandValidatorTests()
        {
            _validator = new UpdateActorCommandValidator();
        }

        [Fact]
        public void WhenValidInputIsGiven_ValidatorShouldNotReturnError()
        {
            var command = new UpdateActorCommand
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenIdIsInvalid_ValidatorShouldReturnError(int invalidId)
        {
            var command = new UpdateActorCommand
            {
                Id = invalidId,
                FirstName = "John",
                LastName = "Doe"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Actor Id sıfırdan büyük olmalıdır.");
        }

        [Theory]
        [InlineData("", "Oyuncunun adı boş olamaz.")]
        [InlineData("J", "Oyuncu adı en az 2 karakter olmalıdır.")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "Oyuncu adı en fazla 50 karakter olabilir.")]
        public void WhenFirstNameIsInvalid_ValidatorShouldReturnError(string firstName, string expectedMessage)
        {
            var command = new UpdateActorCommand
            {
                Id = 1,
                FirstName = firstName,
                LastName = "Doe"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                .WithErrorMessage(expectedMessage);
        }

        [Theory]
        [InlineData("", "Oyuncunun soyadı boş olamaz.")]
        [InlineData("D", "Oyuncu soyadı en az 2 karakter olmalıdır.")]
        [InlineData("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB", "Oyuncu soyadı en fazla 50 karakter olabilir.")]
        public void WhenLastNameIsInvalid_ValidatorShouldReturnError(string lastName, string expectedMessage)
        {
            var command = new UpdateActorCommand
            {
                Id = 1,
                FirstName = "John",
                LastName = lastName
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(expectedMessage);
        }
    }
}