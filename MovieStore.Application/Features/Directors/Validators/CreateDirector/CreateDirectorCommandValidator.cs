using FluentValidation;
using MovieStore.Application.Features.Directors.Commands.CreateDirector;

namespace MovieStore.Application.Features.Directors.Validators.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Yönetmen adı boş olamaz.")
                .MinimumLength(2).WithMessage("Yönetmen adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yönetmen soyadı boş olamaz.")
                .MinimumLength(2).WithMessage("Yönetmen soyadı en az 2 karakter olmalıdır.");
        }
    }
}
