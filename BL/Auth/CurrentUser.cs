using Website.BL.General;
using Website.DAL;
using Website.DAL.Models;

namespace Website.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL UserTokenDAL,
            IProfileDAL profileDAL
            )
        {
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = UserTokenDAL;
            this.profileDAL = profileDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
            {
                return null;
            }

            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (tokenGuid == null)
            {
                return null;
            }

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
            return userid;
        }

        public async Task<bool> IsLoggedIn()
        {
            bool IsLoggedIn = await dbSession.IsLoggedIn();
            if (!IsLoggedIn)
            {
                int? userid = await GetUserIdByToken();
                if (userid != null)
                {
                    await dbSession.SetUserId((int)userid);
                    IsLoggedIn = true;
                }
            }
            return IsLoggedIn;
        }

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSession.GetUserId();
        }

        public async Task<IEnumerable<ProfileModel>> GetProfiles()
        {
            int? userid = await GetCurrentUserId();
            if (userid == null)
            {
                throw new ArgumentNullException("Пользователь не найден");
            }
            return await profileDAL.GetByUserId((int)userid);
        }
    }
}

