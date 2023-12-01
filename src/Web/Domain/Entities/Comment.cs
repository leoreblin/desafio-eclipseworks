namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public record Comment(Guid TaskId, Guid UserId, string Content)
    {
        public static Comment Create(Guid taskId, Guid userId, string content)
        {
            return new Comment(taskId, userId, content);
        }
    }
}
