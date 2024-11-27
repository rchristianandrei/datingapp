using api.Entities;
using api.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositories
{
    public class AppUserRepo(string connString) : IAppUsersRepo
    {
        private static readonly string tblName = "tblappusers";

        private readonly string _connString = connString;

        private const string colId = "id";
        private const string colUsername = "dUsername";
        private const string colPasswordHash = "dPasswordHash";
        private const string colPasswordSalt = "dPasswordSalt";

        #region Create
        public async Task Insert(AppUser user)
        {
            var query = $"INSERT INTO {tblName}({colId}, {colUsername}, {colPasswordHash}, {colPasswordSalt})" +
                "VALUES(@Id, @Username, @Hash, @Salt);";
            MySqlParameter[] parameters = [
                new MySqlParameter("@Id", user.Id),
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
        #endregion

        #region Update
        #endregion

        #region Read
        public async Task<List<AppUser>> Get1000()
        {
            var list = new List<AppUser>();
            var query = $"SELECT {colId}, {colUsername}, {colPasswordHash}, {colPasswordSalt} FROM {tblName} LIMIT 1000;";
            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var user = new AppUser()
                {
                    Id = reader.GetInt32(reader.GetOrdinal(colId)),
                    Username = reader.GetString( reader.GetOrdinal(colUsername))
                };

                if (!reader.IsDBNull(reader.GetOrdinal(colPasswordHash)))
                    user.PasswordHash = (byte[])reader.GetValue(reader.GetOrdinal(colPasswordHash));

                if (!reader.IsDBNull(reader.GetOrdinal(colPasswordSalt)))
                    user.PasswordSalt = (byte[])reader.GetValue(reader.GetOrdinal(colPasswordSalt));

                list.Add(user);
            }
            return list;
        }

        public async Task<AppUser?> Get1(int id)
        {
            var query = $"SELECT {colId}, {colUsername} FROM {tblName} WHERE {colId} = @Id;";
            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Id", id);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                return new AppUser()
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1)
                };
            }
            return null;
        }

        public async Task<bool> UsernameExists(string username)
        {
            var query = $"SELECT {colUsername} FROM {tblName} WHERE {colUsername} = @Username;";
            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Username", username);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }
        #endregion

        #region Delete
        public async Task<int> Delete(int id)
        {
            var query = $"DELETE FROM {tblName} WHERE {colId} = @Id;";
            MySqlParameter[] parameters = [new MySqlParameter("@Id", id)];

            using var conn = new MySqlConnection(_connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
