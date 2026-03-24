using Bet.Application.Abstractions;

namespace Bet.Application.Accounts.Register;

public record RegisterUserCommand(string UserName, string Email, string Password) : ICommand;