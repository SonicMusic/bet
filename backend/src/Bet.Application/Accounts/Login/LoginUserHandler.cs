using Bet.Application.Abstractions;
using Bet.Application.Accounts.Models;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Accounts.Login;

public class LoginUserHandler : ICommandHandler<string, LoginUserCommand>
{
    private readonly UserManager<User> _manager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(
        UserManager<User> manager,
        ITokenProvider tokenProvider,
        ILogger<LoginUserHandler> logger)
    {
        _manager = manager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }


    public async Task<Result<string, Error>> Handle(
        LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _manager.FindByEmailAsync(command.Email);
        if (user is null)
            return Errors.General.NotFound();

        var passwordConfirmed = await _manager.CheckPasswordAsync(user, command.Password);
        if (passwordConfirmed == false)
            return Errors.General.ValueIsInvalid();

        var token = _tokenProvider.GenerateAccessToken(user);
        
        _logger.LogInformation("Successfully logged in.");

        return token;
    }
}