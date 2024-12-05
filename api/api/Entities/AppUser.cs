namespace api.Entities
{
    public class AppUser
    {
        public required string Username { get; set; }

        public byte[] PasswordHash { get; set; } = [];

        public byte[] PasswordSalt { get; set; } = [];
    }
}
