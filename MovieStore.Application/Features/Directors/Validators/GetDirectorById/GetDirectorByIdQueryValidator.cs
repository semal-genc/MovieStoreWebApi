using FluentValidation;
using MovieStore.Application.Features.Directors.Queries.GetDirectorById;

namespace MovieStore.Application.Features.Directors.Validators.GetDirectorById
{
    public class GetDirectorByIdQueryValidator : AbstractValidator<GetDirectorByIdQuery>
    {
        public GetDirectorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Yönetmen id 0'dan büyük olmalıdır.");
        }
    }
}
