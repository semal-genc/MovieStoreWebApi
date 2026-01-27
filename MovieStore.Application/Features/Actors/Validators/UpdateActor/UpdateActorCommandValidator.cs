using FluentValidation;
using MovieStore.Application.Features.Actors.Commands.UpdateActor;

namespace MovieStore.Application.Features.Actors.Validators.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
            
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}