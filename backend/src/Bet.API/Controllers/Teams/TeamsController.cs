using Bet.Application.Teams;
using Bet.Contracts.Requests.Teams;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Teams;

[ApiController]
[Route("[controller]")]
public class TeamsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTeamRequest request,
        [FromServices] CreateTeamHandler handler,
        CancellationToken cancellationToken = default)
    {
        await handler.Handle(request, cancellationToken);
        
        return Ok();
    }
}