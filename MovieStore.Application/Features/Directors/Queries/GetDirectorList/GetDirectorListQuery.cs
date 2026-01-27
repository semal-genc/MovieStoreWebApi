using MediatR;
using MovieStore.Application.Features.Directors.Dtos;

namespace MovieStore.Application.Features.Directors.Queries.GetDirectorList
{
    public class GetDirectorListQuery : IRequest<List<DirectorDto>>
    {
    }
}
