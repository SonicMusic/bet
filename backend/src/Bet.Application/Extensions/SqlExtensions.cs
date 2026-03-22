using System.Data;
using System.Text;
using Dapper;

namespace Bet.Application.Games;

public static class SqlExtensions
{
    public static void ApplySorting(
        this StringBuilder sqlBuilder,
        string? sortBy,
        string? sortDirection)
    {
        if (string.IsNullOrWhiteSpace(sortBy) || string.IsNullOrWhiteSpace(sortDirection)) return;

        var validSortDirections = new[] { "asc", "desc" };

        if (validSortDirections.Contains(sortDirection.ToLower()))
        {
            sqlBuilder.Append($"\norder by {sortBy} {sortDirection}");
        }
        else
        {
            throw new ArgumentException("Invalid sort parameters");
        }
    }

    public static void ApplyPagination(
        this StringBuilder sqlBuilder,
        DynamicParameters parameters,
        int page,
        int pageSize)
    {
        parameters.Add("@PageSize", pageSize, DbType.Int32);
        parameters.Add("@Offset", (page - 1) * pageSize, DbType.Int32);

        sqlBuilder.Append(" LIMIT @PageSize OFFSET @Offset");
    }
}