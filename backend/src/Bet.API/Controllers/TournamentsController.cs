using Bet.API.Extensions;
using Bet.API.Requests.Tournaments;
using Bet.Application.Tournaments;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers;

public class TournamentsController: ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateTournamentRequest request,
        [FromServices] CreateTournamentRequestValidator validator,
        [FromServices] CreateTournamentHandler handler, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new CreateTournamentCommand(request.Name, request.Nation);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}