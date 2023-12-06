using MediatR;

namespace DesafioEclipseworks.WebAPI.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
