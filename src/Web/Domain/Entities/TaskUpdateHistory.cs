namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class TaskUpdateHistory : BaseEntity
    {
        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }

        public string TaskDetails { get; set; } = string.Empty;

        public string UserComment { get; set; } = string.Empty;

        public string UpdateInformation { get; set; } = string.Empty;
    }
}
