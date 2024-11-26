using api.Entities;
using api.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Controllers.AppUsers
{
    public class AppUserRepo(string connString) : IAppUserRepo
    {
        private static readonly string tblName = "tblappusers";

        private readonly string _connString = connString;

        #region Create
        public async Task Save(AppUser user)
        {
            var query = $"INSERT INTO {tblName}(id, Username)" +
                "VALUES(@Id, @Username);";
            MySqlParameter[] parameters = [
                new MySqlParameter("@Id", user.Id),
                new MySqlParameter("@Username", user.Username)];

            using var conn = new MySqlConnection(this._connString);
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
            var query = $"SELECT id, Username FROM {tblName} LIMIT 1000;";
            using var conn = new MySqlConnection(this._connString);
            using var command = new MySqlCommand(query, conn);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new AppUser() { 
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1)
                });
            }
            return list;
        }
        public async Task<AppUser?> Get1(int id)
        {
            var query = $"SELECT id, Username FROM {tblName} WHERE id = @Id;";
            using var conn = new MySqlConnection(this._connString);
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
        #endregion

        #region Delete
        public async Task<int> Delete(int id)
        {
            var query = $"DELETE FROM {tblName} WHERE id = @Id;";
            MySqlParameter[] parameters = [new MySqlParameter("@Id", id)];

            using var conn = new MySqlConnection(this._connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            return await command.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
