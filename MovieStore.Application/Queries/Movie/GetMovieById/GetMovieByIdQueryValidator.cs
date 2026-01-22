using FluentValidation;

namespace MovieStore.Application.Queries.Movie.GetMovieById
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