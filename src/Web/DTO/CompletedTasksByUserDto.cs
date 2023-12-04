namespace DesafioEclipseworks.WebAPI.DTO
{
    public record CompletedTasksByUserDto
    {
        public Guid UserId { get; set; }

        public int Count { get; set; }
    }
}
