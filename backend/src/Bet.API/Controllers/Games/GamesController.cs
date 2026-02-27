using Bet.API.Extensions;
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
        [FromServices] CreateGameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateGameCommand(request.HomeTeamId, request.AwayTeamId);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid guid,
        [FromBody] UpdateGameRequest request,
        [FromServices] UpdateGameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateGameCommand(guid, request.HomeTeamId, request.AwayTeamId);

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