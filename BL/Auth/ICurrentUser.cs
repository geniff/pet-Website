using Website.DAL.Models;

namespace Website.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
        Task<int?> GetCurrentUserId();
        Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}
