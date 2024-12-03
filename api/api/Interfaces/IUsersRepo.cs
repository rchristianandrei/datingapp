using api.DTOs;

namespace api.Interfaces
{
    public interface IUsersRepo
    {
        Task SaveAsync(UserDTO user);

        Task<UserDTO?> GetbyUsernameAsync(string username);

        Task<IEnumerable<UserDTO>> GetAsync();
    }
}
