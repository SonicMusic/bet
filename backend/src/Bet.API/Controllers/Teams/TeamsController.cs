using Bet.API.Extensions;
using Bet.Application.Teams;
using Bet.Contracts.Commands.Teams;
using Bet.Contracts.Requests.Teams;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Teams;

public class TeamsController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateTeamRequest request,
        [FromServices] CreateTeamHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateTeamCommand(request.Name);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Guid>> Update(
        [FromRoute] Guid guid,
        [FromBody] UpdateTeamRequest request,
        [FromServices] UpdateTeamHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateTeamCommand(guid, request.Name);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid guid,
        [FromServices] DeleteTeamHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteTeamCommand(guid);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}