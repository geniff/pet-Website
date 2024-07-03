using Npgsql.PostgresTypes;
using Website.DAL.Models;

namespace Website.BL.AboutOfStudent
{
    public interface IAboutOfStudent
    {
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<AboutOfStudentModel> Get(int profileId);
    }
}