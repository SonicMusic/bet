using Bet.Application.Accounts.Login;

namespace Bet.API.Controllers.Accounts;

public record LoginUserRequest(string Email, string Password)
{
    public LoginUserCommand ToCommand(string email, string password) => new (Email, Password);
};