using FluentValidation;
using MovieStore.Application.Features.Movies.Queries.GetMovieById;

namespace MovieStore.Application.Features.Movies.Validators.GetMovieById
{
    public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Film Id değeri 0'dan büyük olmalıdır.");
        }
    }
}