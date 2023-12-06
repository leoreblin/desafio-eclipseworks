using DesafioEclipseworks.WebAPI.Domain.Shared;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Errors
{
    public static class ReportErrors
    {
        public static Error UserHasNoPermission(Guid userId) =>
            new("Report", $"O usuário de ID {userId} não possui permissão para gerar relatórios.");
    }
}
