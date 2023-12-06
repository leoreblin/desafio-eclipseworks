using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Entities.Users;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.DTO;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using PdfSharpCore.Pdf;
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DesafioEclipseworks.WebAPI.Application.Reports
{
    public class ReportService
    {
        private readonly ITaskUpdateHistoryRepository _taskUpdateHistoryRepository;
        private readonly ITaskRepository _taskRepository;

        public ReportService(
            ITaskUpdateHistoryRepository taskUpdateHistoryRepository,
            ITaskRepository taskRepository)
        {
            _taskUpdateHistoryRepository = taskUpdateHistoryRepository;
            _taskRepository = taskRepository;
        }

        public async Task<Result<byte[]>> GetReportFileAsync(GenerateReportRequest request)
        {
            if (request.Role != UserRole.Manager)
            {
                return ReportErrors.UserHasNoPermission(request.UserId);
            }

            var document = new PdfDocument();

            var tasks = await GetCompletedTasksOfLast30Days();

            string htmlContent = GetHtmlContent(request.UserId, tasks);
            PdfGenerator.AddPdfPages(document, htmlContent, PdfSharpCore.PageSize.A4);
            using var ms = new MemoryStream();
            document.Save(ms);
            byte[]? response = ms.ToArray();

            return await Task.FromResult(response);
        }

        private async Task<List<CompletedTasksByUserDto>> GetCompletedTasksOfLast30Days()
        {
            var completedTasks = await _taskRepository.GetAllCompletedTasksAsync();
            var updatedTasksOverLast30Days = await _taskUpdateHistoryRepository.GetTaskUpdateHistoriesOverLast30Days();

            if (completedTasks is null || updatedTasksOverLast30Days is null)
            {
                return [];
            }

            var tasksByUser = new List<CompletedTasksByUserDto>();

            var groupedByUser = updatedTasksOverLast30Days.GroupBy(t => t.UpdatedBy);
            foreach (var group in groupedByUser)
            {
                var desiredTasks = new List<TaskEntity>();
                foreach (var item in group)
                {
                    desiredTasks.AddRange(completedTasks.Where(t => t.Id == item.TaskId && t.Status == Status.Done) ?? []);
                }

                tasksByUser.Add(new CompletedTasksByUserDto
                {
                    UserId = group.Key,
                    Count = desiredTasks.Count
                });
            }

            return tasksByUser;
        }

        private static string GetHtmlContent(Guid managerId, List<CompletedTasksByUserDto> tasks)
        {
            StringBuilder htmlContent = new();

            htmlContent.AppendLine("<h1>Relatório de Desempenho</h1>");
            htmlContent.AppendLine("<h2>Este relatório relaciona o número de tarefas concluídas por cada usuário nos últimos 30 dias.</h2>");
            htmlContent.AppendLine($"<h3>Gerado pelo usuário de ID: {managerId}</h3>");
            htmlContent.AppendLine($"<h3>Data e hora: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</h3>");
            htmlContent.AppendLine("<div>");
            htmlContent.AppendLine("<table style='width: 100%; border: 1px solid #000'>");
            htmlContent.AppendLine("<thead style='font-weight: bold'>");
            htmlContent.AppendLine("<tr>");
            htmlContent.AppendLine("<td style='border: 1px solid #000'>Usuário ID</td>");
            htmlContent.AppendLine("<td style='border: 1px solid #000'>No. Tarefas</td>");
            htmlContent.AppendLine("</tr>");
            htmlContent.AppendLine("</thead>");

            htmlContent.AppendLine("<tbody>");

            foreach (var task in tasks)
            {
                htmlContent.AppendLine("<tr>");
                htmlContent.AppendLine($"<td style='border: 1px solid #000'>{task.UserId}</td>");
                htmlContent.AppendLine($"<td style='border: 1px solid #000'>{task.Count}</td>");
                htmlContent.AppendLine("</tr>");
            }

            htmlContent.AppendLine("</tbody>");
            htmlContent.AppendLine("</table>");
            htmlContent.AppendLine("</div>");

            return htmlContent.ToString();
        }
    }
}
