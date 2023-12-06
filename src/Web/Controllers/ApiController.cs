using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEclipseworks.WebAPI.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender Sender;

        public ApiController(ISender sender)
        {
            Sender = sender;
        }
    }
}
