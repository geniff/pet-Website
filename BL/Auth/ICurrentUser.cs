﻿namespace Website.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedIn();
    }
}
