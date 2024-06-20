using System.ComponentModel.DataAnnotations;
using Website.DAL.Models;

namespace Website.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(Website.DAL.Models.UserModel user);

        Task<int> Authentificate(string email, string password, bool rememberMe);

        Task ValidateEmail(string email);

        Task Register(UserModel user);
    }
}
