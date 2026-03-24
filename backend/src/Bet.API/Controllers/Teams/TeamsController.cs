using Bet.API.Extensions;
using Bet.API.Requests.Teams;
using Bet.Application.Teams.Commands;
using Bet.Application.Teams.Commands.Create;
using Bet.Application.Teams.Commands.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Teams;

public class TeamsController : ApplicationController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateTeamRequest request,
        [FromServices] CreateTeamHandler handler,
        [FromServices] CreateTeamRequestValidator validator,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new CreateTeamCommand(request.Name.Trim());
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{guid:guid}")]
    public async Task<ActionResult<Guid>> UpdateName(
        [FromRoute] Guid guid,
        [FromBody] UpdateTeamNameRequest request,
        [FromServices] UpdateTeamNameRequestValidator validator,
        [FromServices] UpdateTeamNameHandler handler,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
            return BadRequest(validationResult.Errors);
        
        var command = new UpdateTeamNameCommand(guid, request.Name.Trim());

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpPost("{guid:guid}/logo")]
    public async Task<ActionResult<Guid>> UploadTeamLogo(
        [FromRoute] Guid guid,
        IFormFile file,
        [FromServices] UploadTeamLogoHandler handler, 
        CancellationToken cancellationToken)
    {
        await using var stream = file.OpenReadStream();

        var command = new UploadLogoTeamCommand(guid, stream);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}