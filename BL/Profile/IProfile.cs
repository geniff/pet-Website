using Website.DAL.Models;

namespace Website.BL.Profile
{
    public interface IProfile
    {
        Task<IEnumerable<ProfileModel>> Get(int userId);
        Task AddOrUpdate(ProfileModel model);

    }
}
