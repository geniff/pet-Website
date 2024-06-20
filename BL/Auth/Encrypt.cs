using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Website.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        //Password to hash
        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
                ));
            throw new NotImplementedException();
        }
    }
}
