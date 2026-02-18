using Bet.Contracts.Requests;
using Bet.Domain.GameManagement;
using Bet.Domain.TeamManagement;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateGameRequest request)
    {
        var homeTeam = Team.Create(request.HomeTeamName).Value.Id;
        var awayTeam = Team.Create(request.AwayTeamName).Value.Id;
        var game = Game.Create(homeTeam, awayTeam);
        
        if (game.IsFailure)
            return BadRequest(game.Error);
        
        return Ok(game.Value);
    }
}