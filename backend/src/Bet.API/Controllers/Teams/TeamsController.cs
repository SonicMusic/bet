using Bet.API.Extensions;
using Bet.Application.Teams;
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
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}