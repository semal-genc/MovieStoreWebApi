using FluentValidation;
using MovieStore.Application.Features.Movies.Commands.DeleteMovie;

namespace MovieStore.Application.Features.Movies.Validators.DeleteMovie
{
    public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Geçerli bir film Id'si girilmelidir.");
        }
    }
}