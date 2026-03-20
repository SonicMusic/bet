using Bet.API.Extensions;
using Bet.API.Requests.Games;
using Bet.API.Requests.Predictions;
using Bet.Application.Games.Create;
using Bet.Application.Games.Delete;
using Bet.Application.Games.Update;
using Bet.Application.Predictions.Create;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers;

public class GamesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateGameRequest request,
        [FromServices] CreateGameRequestValidator validator,
        [FromServices] CreateGameHandler createGameHandler,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
       
        var command = new CreateGameCommand(request.HomeTeam, request.AwayTeam);
        
        var result = await createGameHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    //todo удалить
    [HttpPut("{guid:guid}/info")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid guid,
        [FromBody] UpdateGameRequest request,
        [FromServices] UpdateGameRequestValidator validator,
        [FromServices] UpdateGameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new UpdateGameCommand(guid, request.HomeTeam, request.AwayTeam);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid guid,
        [FromServices] DeleteGameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteGameCommand(guid);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("{gameId:guid}/prediction")]
    public async Task<ActionResult<Guid>> AddPrediction(
        [FromRoute] Guid gameId,
        [FromBody] CreatePredictionRequest request,
        [FromServices] CreatePredictionRequestValidator validator,
        [FromServices] CreatePredictionHandler handler,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new CreatePredictionCommand(gameId, request.HomeTeamGoals, request.AwayTeamGoals);

        var prediction = await handler.Handle(command, cancellationToken);
        if (prediction.IsFailure)
            return prediction.Error.ToResponse();

        return Ok(prediction.Value);
    }
    
    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Guid>> SetStart(
        [FromRoute] Guid guid,
        [FromBody] SetStartGameRequest request,
        [FromServices] SetStartGameRequestValidator validator,
        [FromServices] SetStartGameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new SetStartGameCommand(guid, request.DateTimeOffset);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}