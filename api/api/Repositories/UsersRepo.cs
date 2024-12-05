using api.DTOs;
using api.Interfaces;
using MySql.Data.MySqlClient;

namespace api.Repositories
{
    public class UsersRepo(string connString) : IUsersRepo
    {
        private readonly string _connString = connString;

        private const string tblName = "tblusers";

        private const string colUsername = "dUsername";
        private const string colName = "dName";
        private const string colGender = "dGender";
        private const string colDateOfBirth = "tDateOfBirth";
        private const string colIntroduction = "dIntroduction";
        private const string colLookingFor = "dLookingFor";
        private const string colInterests = "dInterests";
        private const string colCity = "dCity";
        private const string colCountry = "dCountry";
        private const string colCreated = "tCreated";
        private const string colLastActive = "tLastActive";

        public async Task SaveAsync(UserDTO user)
        {
            var query = $"INSERT INTO {tblName}({colUsername}, {colName}, {colGender}, {colDateOfBirth}, {colIntroduction}," +
                $"{colLookingFor}, {colInterests}, {colCity}, {colCountry})" +
                $"VALUES(@Username, @Name, @Gender, @DateOfBirth, @Introduction, @LookingFor, @Interests, @City, @Country)" +
                $"ON DUPLICATE KEY UPDATE " +
                $"{colUsername} = @Username, {colName} = @Name, {colGender} = @Gender, {colDateOfBirth} = @DateOfBirth, {colIntroduction} = @Introduction," +
                $"{colLookingFor} = @LookingFor, {colInterests} = @Interests, {colCity} = @City, {colCountry} = @Country, {colLastActive} = CURRENT_TIMESTAMP();";

            MySqlParameter[] parameters = [
                new ("@Username", user.Username),
                new ("@Name", user.Name),
                new ("@Gender", user.Gender),
                new ("@DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd")),
                new ("@Introduction", user.Introduction),
                new ("@LookingFor", user.LookingFor),
                new ("@Interests", user.Interests),
                new ("@City", user.City),
                new ("@Country", user.Country)
            ];

            using var conn = new MySqlConnection(this._connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddRange(parameters);
            await conn.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<UserDTO?> GetbyUsernameAsync(string username)
        {
            var query = $"SELECT {colName}, {colGender}, {colDateOfBirth}, {colIntroduction}," +
                $"{colLookingFor}, {colInterests}, {colCity}, {colCountry}, {colCreated}, {colLastActive} " +
                $"FROM {tblName} WHERE LOWER({colUsername}) = LOWER(@Username);";

            using var conn = new MySqlConnection(this._connString);
            using var command = new MySqlCommand(query, conn);
            command.Parameters.AddWithValue("@Username", username);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dob = (DateTime)reader[colDateOfBirth];
                return new UserDTO
                {
                    Name = (string)reader[colName],
                    Gender = (string)reader[colGender],
                    Age = this.CalculateAge(dob),
                    DateOfBirth = dob,
                    Introduction = (string)reader[colIntroduction],
                    LookingFor = (string)reader[colLookingFor],
                    Interests = (string)reader[colInterests],
                    City = (string)reader[colCity],
                    Country = (string)reader[colCountry],
                    Created = (DateTime)reader[colCreated],
                    LastActive = (DateTime)reader[colLastActive]
                };
            }
            return null;
        }

        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            var query = $"SELECT {colName}, {colGender}, {colDateOfBirth}, {colIntroduction}," +
                $"{colLookingFor}, {colInterests}, {colCity}, {colCountry}, {colCreated}, {colLastActive} " +
                $"FROM {tblName};";

            var list = new List<UserDTO>();

            using var conn = new MySqlConnection(this._connString);
            using var command = new MySqlCommand(query, conn);
            await conn.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var dob = (DateTime)reader[colDateOfBirth];
                list.Add(new UserDTO
                {
                    Name = (string)reader[colName],
                    Gender = (string)reader[colGender],
                    Age = this.CalculateAge(dob),
                    DateOfBirth = dob,
                    Introduction = (string)reader[colIntroduction],
                    LookingFor = (string)reader[colLookingFor],
                    Interests = (string)reader[colInterests],
                    City = (string)reader[colCity],
                    Country = (string)reader[colCountry],
                    Created = (DateTime)reader[colCreated],
                    LastActive = (DateTime)reader[colLastActive]
                });
            }
            return list;
        }

        private int CalculateAge(DateTime date)
        {
            var today = DateTime.Now;
            var age = today.Year - date.Year;
            if (date.AddYears(age) > today) age--;
            return age;
        }
    }
}
