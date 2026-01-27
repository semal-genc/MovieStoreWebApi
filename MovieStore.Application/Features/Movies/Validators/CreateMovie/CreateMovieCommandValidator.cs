using FluentValidation;
using MovieStore.Application.Features.Movies.Commands.CreateMovie;

namespace MovieStore.Application.Features.Movies.Validators.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty().WithMessage("Film adı boş olamaz")
                .MinimumLength(2).WithMessage("Film adı en az 2 karakter olmalı")
                .MaximumLength(100);

            RuleFor(x=>x.Year)
                .GreaterThan(1900)
                .LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.GenreId)
                .GreaterThan(0);

            RuleFor(x => x.DirectorId)
                .GreaterThan(0);
        }
    }
}