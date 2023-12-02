using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioEclipseworks.WebAPI.Domain.Entities
{
    public class TaskUpdateHistory : BaseEntity
    {
        public Guid TaskId { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string Comment { get; set; } = default!;
    }
}
