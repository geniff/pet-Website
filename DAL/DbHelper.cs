using Dapper;
using Npgsql;

namespace Website.DAL
{
    public class DbHelper
    {
        public static string ConnString = "User ID=postgres;Password=xxx;Host=localhost;Port=5438;Database=test";

        // Executes the query and returns a number
        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            using var connection = new NpgsqlConnection(DbHelper.ConnString);
            await connection.OpenAsync();

            return await connection.ExecuteAsync(sql, model);
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using var connection = new NpgsqlConnection(DbHelper.ConnString);
            await connection.OpenAsync();

            return await connection.QueryAsync<T>(sql, model);
        }
    }
}