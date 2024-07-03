using Website.DAL;
using Website.DAL.Models;

namespace Website.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;
        public Profile(IProfileDAL profileDAL) 
        {
            this.profileDAL = profileDAL;
        }
        public async Task<IEnumerable<ProfileModel>> Get(int userId)
        {
            return await profileDAL.GetByUserId(userId);
        }

        public async Task AddOrUpdate(ProfileModel model)
        {
            if (model.ProfileId == null)
            {
                model.ProfileId = await profileDAL.Add(model);
            }

            else
            {
                await profileDAL.Update(model);
            }
        }
    }
}
