using MovieStore.Application.Features.Movies.Dtos;

namespace MovieStore.Application.Features.Directors.Dtos
{
    public class DirectorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
    }

    public class DirectorDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public List<MovieDto> Movies { get; set; } = new();
    }
}
