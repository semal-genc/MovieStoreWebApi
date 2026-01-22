namespace MovieStore.Application.DTOs
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
    }
    
    public class ActorDetailDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public List<MovieDto> Movies { get; set; } = new();
    }
}
