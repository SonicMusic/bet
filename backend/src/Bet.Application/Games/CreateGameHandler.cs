using Bet.Application.IoC;
using Bet.Contracts.Commands.Games;
using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games;

public class CreateGameHandler
{
    private readonly IGamesRepository _repository;
    private readonly ILogger<CreateGameHandler> _logger;

    public CreateGameHandler(
        IGamesRepository repository,
        ILogger<CreateGameHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateGameCommand command,
        CancellationToken cancellationToken = default)
    {
        var gameResult = Game.Create(command.HomeTeamId, command.AwayTeamId);
        if (gameResult.IsFailure)
            return Errors.General.ValueIsInvalid();

        var game = await _repository.Add(gameResult.Value, cancellationToken);

        _logger.LogInformation("Created game id {game}", game);

        return game;
    }
}