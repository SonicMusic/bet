using Bet.Application.Abstractions;

namespace Bet.Application.Accounts.Login;

public record LoginUserCommand(string Email, string Password) : ICommand;