using FluentValidation.TestHelper;
using MovieStore.Application.Features.Actors.Commands.DeleteActor;
using MovieStore.Application.Features.Actors.Validators.DeleteActor;

namespace MovieStore.Tests.Application.Features.Actors.Validators.DeleteActor
{
    public class DeleteActorCommandValidatorTests
    {
        private readonly DeleteActorCommandValidator _validator;

        public DeleteActorCommandValidatorTests()
        {
            _validator = new DeleteActorCommandValidator();
        }

        [Fact]
        public void WhenIdIsLessThanOrEqualToZero_ValidatorShouldReturnError()
        {
            var command = new DeleteActorCommand(0);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Oyuncu Id değeri 0'dan büyük olmalıdır.");
        }

        [Fact]
        public void WhenIdIsGreaterThanZero_ValidatorShouldNotReturnError()
        {
            var command = new DeleteActorCommand(1);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}