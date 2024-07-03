using Dapper;
using Npgsql;

namespace Website.DAL
{
    public class DbHelper
    {
        public static string ConnString = "User ID=postgres;Password=Egor2007;Host=localhost;Port=5438;Database=test";

        // Executes the query and returns a number
        public static async Task ExecuteAsync(string sql, object model)
        {
            using var connection = new NpgsqlConnection(DbHelper.ConnString);
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public static async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using var connection = new NpgsqlConnection(DbHelper.ConnString);
            await connection.OpenAsync();

            var result = await connection.QueryFirstOrDefaultAsync<T>(sql , model);
            if (result == null)
            {
                throw new Exception();
            }

            else
            {
                return result;
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using var connection = new NpgsqlConnection(DbHelper.ConnString);
            await connection.OpenAsync();

            return await connection.QueryAsync<T>(sql, model);
        }
    }
}