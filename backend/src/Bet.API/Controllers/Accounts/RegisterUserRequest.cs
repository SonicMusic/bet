namespace Bet.API.Controllers.Accounts;

public record RegisterUserRequest(string UserName, string Email, string Password);