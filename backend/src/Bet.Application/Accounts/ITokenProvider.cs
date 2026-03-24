using Bet.Application.Accounts.Models;

namespace Bet.Application.Accounts;

public interface ITokenProvider
{
   string GenerateAccessToken(User user);
}