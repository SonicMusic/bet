using Bet.Application.Abstractions;
using Bet.Application.Accounts.Models;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Bet.Application.Accounts.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _manager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(UserManager<User> manager, ILogger<RegisterUserHandler> logger)
    {
        _manager = manager;
        _logger = logger;
    }
    
    public async Task<UnitResult<Error>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var existUser = await _manager.FindByEmailAsync(command.Email);
        if (existUser != null)
            return Errors.General.AlreadyExist();

        var user = new User
        {
            UserName = command.UserName,
            Email = command.Email,
        };

        var result = await _manager.CreateAsync(user, command.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => 
                Error.Failure(e.Code, e.Description)).ToList();

            return errors.First();
        };
        
        _logger.LogInformation("User {userName} created a new account", user.UserName);

        return Result.Success<Error>();
    }
}