using Bet.API.Extensions;
using Bet.API.Validators.Game;
using Bet.Application.Games;
using Bet.Contracts.Commands.Games;
using Bet.Contracts.Requests.Games;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Games;

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

    [HttpPut("{guid:guid}")]
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
}