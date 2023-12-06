using DesafioEclipseworks.WebAPI.Domain.Shared;
using MediatR;

namespace DesafioEclipseworks.WebAPI.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }
}
