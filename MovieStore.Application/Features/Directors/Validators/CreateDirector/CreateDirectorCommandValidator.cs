using FluentValidation;
using MovieStore.Application.Features.Directors.Commands.CreateDirector;

namespace MovieStore.Application.Features.Directors.Validators.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
