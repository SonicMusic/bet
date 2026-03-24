using Bet.API.Extensions;
using Bet.Application.Accounts.Login;
using Bet.Application.Accounts.Register;
using Microsoft.AspNetCore.Mvc;

namespace Bet.API.Controllers.Accounts;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler, 
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(request.UserName, request.Email, request.Password);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler, 
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(request.Email, request.Password);
        
        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}