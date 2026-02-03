using FluentValidation;
using MovieStore.Application.Features.Directors.Commands.UpdateDirector;

namespace MovieStore.Application.Features.Directors.Validators.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0)
                .WithMessage("Yönetmen Id sıfırdan büyük olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Yönetmenin adı boş olamaz.")
                .MinimumLength(2).WithMessage("Yönetmen adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yönetmenin soyadı boş olamaz.")
                .MinimumLength(2).WithMessage("Yönetmen soyadı en az 2 karakter olmalıdır.");
        }
    }
}
