using FluentValidation;
using MovieStore.Application.Features.Actors.Commands.UpdateActor;

namespace MovieStore.Application.Features.Actors.Validators.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Actor Id sıfırdan büyük olmalıdır.");
            
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Oyuncunun adı boş olamaz.")
                .MinimumLength(2).WithMessage("Oyuncu adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Oyuncu adı en fazla 50 karakter olabilir.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Oyuncunun soyadı boş olamaz.")
                .MinimumLength(2).WithMessage("Oyuncu soyadı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Oyuncu soyadı en fazla 50 karakter olabilir.");
        }
    }
}