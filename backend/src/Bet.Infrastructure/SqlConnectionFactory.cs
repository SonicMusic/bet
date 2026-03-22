using System.Data;
using Bet.Application.IoC;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Bet.Infrastructure;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection Create() =>
        new NpgsqlConnection(_configuration.GetConnectionString("Database"));
}