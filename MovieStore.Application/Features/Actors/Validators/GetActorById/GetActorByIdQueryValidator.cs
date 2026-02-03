using FluentValidation;
using MovieStore.Application.Features.Actors.Queries.GetActorById;

namespace MovieStore.Application.Features.Actors.Validators.GetActorById
{
    public class GetActorByIdQueryValidator : AbstractValidator<GetActorByIdQuery>
    {
        public GetActorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Oyuncu id 0'dan büyük olmalıdır.");
        }
    }
}
