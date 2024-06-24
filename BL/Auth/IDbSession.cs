using System;
using Website.DAL.Models;

namespace Website.BL.Auth
{
    public interface IDbSession
    {
        Task<SessionModel> GetSession();

        Task<int> SetUserId(int userId);

        Task<int?> GetUserId();

        Task<bool> IsLoggedIn();

        Task Lock();

        void ResetSessionCache();
    }
}

