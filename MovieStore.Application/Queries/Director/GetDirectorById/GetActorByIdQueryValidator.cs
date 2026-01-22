using FluentValidation;

namespace MovieStore.Application.Queries.Director.GetDirectorById
{
    public class GetActorByIdDirectorValidator : AbstractValidator<GetDirectorByIdQuery>
    {
        public GetActorByIdDirectorValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
