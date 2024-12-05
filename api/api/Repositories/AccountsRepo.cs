using api.Entities;
using api.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositories
{
    public class AccountsRepo(string connString) : IAccountsRepo
    {
        private readonly string _connString = connString;

        private const string tblName = "tblaccounts";

        private const string colUsername = "dUsername";
        private const string colPasswordHash = "dPasswordHash";
        private const string colPasswordSalt = "dPasswordSalt";

        public async Task InsertAsync(AppUser user)
        {
            var query = $"INSERT INTO {tblName}({colUsername}, {colPasswordHash}, {colPasswordSalt})" +
                "VALUES(@Username, @Hash, @Salt);";
            MySqlParameter[] parameters = [
                new MySqlParameter("@Username", user.Username),
                new MySqlParameter("@Hash", user.PasswordHash),
                new MySqlParameter("@Salt", user.PasswordSalt)
            ];

            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            var query = $"SELECT {colUsername}, {colPasswordHash}, {colPasswordSalt} FROM {tblName} WHERE {colUsername} = @Username;";

            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Username", username);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new AppUser()
                {
                    Username = (string)reader[colUsername],
                    PasswordHash = (byte[])reader[colPasswordHash],
                    PasswordSalt = (byte[])reader[colPasswordSalt]
                };
            }
            return null;
        }

        public async Task<bool> CheckUsernameExistsAsync(string username)
        {
            var query = $"SELECT {colUsername} FROM {tblName} WHERE {colUsername} = @Username;";
            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Username", username);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }

        public async Task DeleteAppUserAsync(string username)
        {
            var query = $"DELETE FROM {tblName} WHERE {colUsername} = @Username;";
            MySqlParameter[] parameters = [new MySqlParameter("@Username", username)];

            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
