using Website.BL.AboutOfStudent;
using Website.DAL;
using Website.DAL.Models;

namespace Website.BL.AboutOfStudent
{
    public class AboutOfStudent : IAboutOfStudent
    {
        private readonly IProfileDAL profileDAL;

        public AboutOfStudent(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        //Searching information in db
        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await profileDAL.Search(top);
        }

        public async Task<AboutOfStudentModel> Get(int profileId)
        {
            ProfileModel profileModel = await this.profileDAL.GetByProfileId(profileId);
            return new AboutOfStudentModel()
            {
                Profile = profileModel
            };
        }
    }
}
