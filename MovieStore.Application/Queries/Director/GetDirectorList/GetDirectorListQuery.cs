using MediatR;
using MovieStore.Application.DTOs;

namespace MovieStore.Application.Queries.Director.GetDirectorList
{
    public class GetDirectorListQuery : IRequest<List<DirectorDto>>
    {
    }
}
