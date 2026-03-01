using Bet.API.Extensions;
using Bet.Application.Predictions;
using Bet.Contracts.Commands.Predictions;
using Bet.Contracts.Requests.Predictions;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Predictions;

public class PredictionsController : ApplicationController
{
    [HttpPost("games/{gameId:guid}")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid gameId,
        [FromBody] CreatePredictionRequest request,
        [FromServices] CreatePredictionHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreatePredictionCommand(gameId, request.HomeTeamGoals, request.HomeTeamGoals);

        var prediction = await handler.Handle(command, cancellationToken);
        if (prediction.IsFailure)
            return prediction.Error.ToResponse();

        return Ok(prediction.Value);
    }

    [HttpPut("{predictionId:guid}")]
    public async Task<ActionResult<Guid>> Create(
        [FromRoute] Guid predictionId,
        [FromBody] UpdateStatusPredictionRequest request,
        [FromServices] UpdateStatusPredictionHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateStatusPredictionCommand(predictionId, request.Status);

        var prediction = await handler.Handle(command, cancellationToken);
        if (prediction.IsFailure)
            return prediction.Error.ToResponse();

        return Ok(prediction.Value);
    }
}