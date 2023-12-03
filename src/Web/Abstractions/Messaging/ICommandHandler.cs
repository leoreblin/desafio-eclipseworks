using DesafioEclipseworks.WebAPI.Domain.Shared;
using MediatR;

namespace DesafioEclipseworks.WebAPI.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }
}
