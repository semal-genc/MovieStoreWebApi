namespace MovieStore.Application.DTOs
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
