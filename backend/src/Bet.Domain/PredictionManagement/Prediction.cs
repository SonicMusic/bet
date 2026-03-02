using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Domain.PredictionManagement;

public class Prediction : Entity, IIsDeletedField
{
    private bool _isDeleted = false;

    // ef core
    private Prediction()
    {
    }

    private Prediction(Guid predictionId, Guid gameId, int homeTeamGoals, int awayTeamGoals)
    {
        Id = predictionId;
        GameId = gameId;
        HomeTeamGoals = homeTeamGoals;
        AwayTeamGoals = awayTeamGoals;
    }

    public new Guid Id { get; private set; }
    public Guid GameId { get; private set; }
    public int HomeTeamGoals { get; private set; } = default;
    public int AwayTeamGoals { get; private set; } = default;
    public StatusPrediction Status { get; private set; } = StatusPrediction.Pending;

    public static Result<Prediction, Error> Create(
        Guid gameId,
        int homeTeamGoals,
        int awayTeamGoals)
    {
        if (string.IsNullOrWhiteSpace(gameId.ToString()))
            return Errors.General.ValueIsRequired();

        if (homeTeamGoals < 0 || awayTeamGoals < 0)
            return Errors.General.ValueIsInvalid();

        var predictionId = Guid.NewGuid();
        return new Prediction(predictionId, gameId, homeTeamGoals, awayTeamGoals);
    }

    public UnitResult<Error> SetStatus(StatusPrediction statusPrediction)
    {
        Status = statusPrediction;

        return UnitResult.Success<Error>();
    }

    public void Delete()
    {
        if (_isDeleted == false)
            _isDeleted = true;
    }

    public void Restore()
    {
        if (!_isDeleted) return;

        _isDeleted = false;
    }
}