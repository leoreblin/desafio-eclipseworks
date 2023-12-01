using DesafioEclipseworks.WebAPI.Domain.Shared;
using MediatR;

namespace DesafioEclipseworks.WebAPI.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
