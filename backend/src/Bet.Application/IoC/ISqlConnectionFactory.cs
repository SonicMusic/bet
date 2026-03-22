using System.Data;

namespace Bet.Application.IoC;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}