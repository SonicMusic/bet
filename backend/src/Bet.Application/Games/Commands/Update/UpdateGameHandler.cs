using Bet.Application.Abstractions;
using Bet.Application.IoC;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Games.Commands.Update;

public class UpdateGameHandler : ICommandHandler<Guid, UpdateGameCommand>
{
    private readonly IGamesRepository _gamesRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly ILogger<UpdateGameHandler> _logger;

    public UpdateGameHandler(
        IGamesRepository gamesRepository,
        ITeamsRepository teamsRepository,
        ILogger<UpdateGameHandler> logger)
    {
        _gamesRepository = gamesRepository;
        _teamsRepository = teamsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateGameCommand command,
        CancellationToken cancellationToken = default)
    {
        var gameFromDb = await _gamesRepository.GetById(command.GameId, cancellationToken);
        if (gameFromDb.IsFailure)
            return gameFromDb.Error;
        
        var homeTeam = await _teamsRepository.GetByName(command.HomeTeam, cancellationToken);
        if (homeTeam.IsFailure)
            return homeTeam.Error;
        
        var awayTeam = await _teamsRepository.GetByName(command.AwayTeam, cancellationToken);
        if (homeTeam.IsFailure)
            return homeTeam.Error;
        
        
        var gameResult = gameFromDb.Value.Update(homeTeam.Value.Id, awayTeam.Value.Id);
        if (gameResult.IsFailure)
            return gameResult.Error;

        await _gamesRepository.Save(gameFromDb.Value, cancellationToken);
        
        _logger.LogInformation("Update game with id {result}", gameFromDb.Value.Id);

        return gameFromDb.Value.Id;
    }
}