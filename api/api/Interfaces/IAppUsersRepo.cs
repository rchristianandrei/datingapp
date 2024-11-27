using api.Entities;

namespace api.Interfaces
{
    public interface IAppUsersRepo
    {

        #region Create
        Task Insert(AppUser user);
        #endregion

        #region Update
        #endregion

        #region Read
        Task<List<AppUser>> Get1000();
        Task<AppUser?> Get1(int id);
        Task<bool> UsernameExists(string username);
        #endregion

        #region Delete
        Task<int> Delete(int id);
        #endregion
    }
}
