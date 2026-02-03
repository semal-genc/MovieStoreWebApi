using FluentValidation;
using MovieStore.Application.Features.Actors.Commands.DeleteActor;

namespace MovieStore.Application.Features.Actors.Validators.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Oyuncu Id değeri 0'dan büyük olmalıdır.");
        }
    }
}
