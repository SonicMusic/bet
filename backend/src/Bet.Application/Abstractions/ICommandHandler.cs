using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application.Abstractions;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, Error>> Handle(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<Error>> Handle(TCommand command, CancellationToken cancellationToken = default);
}