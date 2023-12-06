using DesafioEclipseworks.WebAPI.Application.Reports;
using DesafioEclipseworks.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("api/v1/users/tasks/reports/completed-tasks")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GenerateTaskReport(GenerateReportRequest request)
        {
            var response = await _reportService.GetReportFileAsync(request);

            if (response.IsFailure)
            {
                return BadRequest(response.Error);
            }

            if (response.Value is null)
            {
                return BadRequest();
            }

            string fileName = "Relatorio-desempenho.pdf";

            return File(response.Value, "application/pdf", fileName);
        }
    }
}
