using Bet.API.Extensions;
using Bet.Application.Predictions;
using Bet.Contracts.Commands.Predictions;
using Bet.Contracts.Requests.Predictions;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Predictions;

public class PredictionsController : ApplicationController
{
    [HttpPost("games/{gameId}")]
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

    // [HttpPut("{guid:guid}")]
    // public async Task<ActionResult<Guid>> Update(
    //     [FromRoute] Guid guid,
    //     [FromBody] UpdateGameRequest request,
    //     [FromServices] UpdateGameHandler handler,
    //     CancellationToken cancellationToken = default)
    // {
    //     var command = new UpdateGameCommand(guid, request.HomeTeamId, request.AwayTeamId);
    //
    //     var result = await handler.Handle(command, cancellationToken);
    //     if (result.IsFailure)
    //         return result.Error.ToResponse();
    //
    //     return Ok(result.Value);
    // }
    //
    // [HttpDelete("{guid:guid}")]
    // public async Task<ActionResult<Guid>> Delete(
    //     [FromRoute] Guid guid,
    //     [FromServices] DeleteGameHandler handler,
    //     CancellationToken cancellationToken = default)
    // {
    //     var command = new DeleteGameCommand(guid);
    //
    //     var result = await handler.Handle(command, cancellationToken);
    //     if (result.IsFailure)
    //         return result.Error.ToResponse();
    //
    //     return Ok(result.Value);
    // }
}