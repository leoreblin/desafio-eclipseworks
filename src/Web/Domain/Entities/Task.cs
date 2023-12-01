namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class Task : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string Details { get; set; } = default!;
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; }

        public Task(string title, string details, DateTime dueDate, Status status, Priority priority)
        {
            Title = title;
            Details = details;
            DueDate = dueDate;
            Status = status;
            Priority = priority;
        }
    }
}
