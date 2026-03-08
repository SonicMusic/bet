using Bet.Application.IoC;
using Bet.Contracts.Commands.Games;
using Bet.Domain.GameManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games;

public class CreateGameHandler
{
    private readonly IGamesRepository _gamesRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ILogger<CreateGameHandler> _logger;

    public CreateGameHandler(
        IGamesRepository gamesRepository,
        ITeamsRepository teamsRepository,
        ILogger<CreateGameHandler> logger)
    {
        _gamesRepository = gamesRepository;
        _teamsRepository = teamsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateGameCommand command,
        CancellationToken cancellationToken = default)
    {
        var homeTeam = await _teamsRepository.GetByName(command.HomeTeam, cancellationToken);
        if (homeTeam.IsFailure)
            return homeTeam.Error;
        
        var awayTeam = await _teamsRepository.GetByName(command.AwayTeam, cancellationToken);
        if (homeTeam.IsFailure)
            return homeTeam.Error;
        
        var gameResult = Game.Create(command.TournamentId, homeTeam.Value.Id, awayTeam.Value.Id);
        if (gameResult.IsFailure)
            return Errors.General.ValueIsInvalid();

        var game = await _gamesRepository.Add(gameResult.Value, cancellationToken);

        _logger.LogInformation("Created game id {game}", game);

        return game;
    }
}