using Bet.API.Extensions;
using Bet.API.Validators.Team;
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