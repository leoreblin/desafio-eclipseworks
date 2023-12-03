using DesafioEclipseworks.WebAPI.Domain.Shared;
using MediatR;

namespace DesafioEclipseworks.WebAPI.Abstractions.Messaging
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
