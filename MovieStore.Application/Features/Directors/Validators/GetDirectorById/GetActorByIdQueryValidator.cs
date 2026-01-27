using FluentValidation;
using MovieStore.Application.Features.Directors.Queries.GetDirectorById;

namespace MovieStore.Application.Features.Directors.Validators.GetDirectorById
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
