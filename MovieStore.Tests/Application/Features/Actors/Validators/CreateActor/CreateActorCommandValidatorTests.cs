using FluentValidation.TestHelper;
using MovieStore.Application.Features.Actors.Commands.CreateActor;
using MovieStore.Application.Features.Actors.Validators.CreateActor;

namespace MovieStore.Tests.Application.Features.Actors.Validators.CreateActor
{
    public class CreateActorCommandValidatorTests
    {
        private readonly CreateActorCommandValidator _validator;

        public CreateActorCommandValidatorTests()
        {
            _validator = new CreateActorCommandValidator();
        }

        [Fact]
        public void WhenValidInputIsGiven_ValidatorShouldNotReturnError()
        {
            var command = new CreateActorCommand
            {
                FirstName = "John",
                LastName = "Doe"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("", "Oyuncunun adı boş olamaz.")]
        [InlineData("J", "Oyuncu adı en az 2 karakter olmalıdır.")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "Oyuncu adı en fazla 50 karakter olabilir.")]
        public void WhenFirstNameIsInvalid_ValidatorShouldReturnError(string firstName, string expectedMessage)
        {
            var command = new CreateActorCommand
            {
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
            var command = new CreateActorCommand
            {
                FirstName = "John",
                LastName = lastName
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.LastName)
                .WithErrorMessage(expectedMessage);
        }
    }
}