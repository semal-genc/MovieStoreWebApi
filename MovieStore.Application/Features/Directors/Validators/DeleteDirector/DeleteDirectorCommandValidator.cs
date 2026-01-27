using FluentValidation;
using MovieStore.Application.Features.Directors.Commands.DeleteDirector;

namespace MovieStore.Application.Features.Directors.Validators.DeleteDirector
{
    public class DeleteDirectorCommandValidator : AbstractValidator<DeleteDirectorCommand>
    {
        public DeleteDirectorCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
