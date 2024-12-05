using api.Entities;

namespace api.Interfaces
{
    public interface IAccountsRepo
    {
        Task InsertAsync(AppUser user);

        Task<AppUser?> GetByUsernameAsync(string username);

        Task<bool> CheckUsernameExistsAsync(string username);

        Task DeleteAppUserAsync(string username);
    }
}
