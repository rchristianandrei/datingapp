using api.Entities;

namespace api.Interfaces
{
    public interface IAppUserRepo
    {

        #region Create / Update
        Task Save(AppUser user);
        #endregion

        #region Read
        Task<List<AppUser>> Get1000();
        Task<AppUser?> Get1(int id);
        #endregion

        #region Delete
        Task<int> Delete(int id);
        #endregion
    }
}
