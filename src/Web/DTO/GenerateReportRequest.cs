using DesafioEclipseworks.WebAPI.Domain.Entities.Users;

namespace DesafioEclipseworks.WebAPI.DTO
{
    public record GenerateReportRequest(Guid UserId, UserRole Role);
}
