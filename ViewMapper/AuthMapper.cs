using Website.ViewModels;
using Website.DAL.Models;
using System;
using Website.BL.Auth;
using System.Net;
namespace Website.ViewMapper
{
    public class AuthMapper
    {
        public static UserModel MapRegistrationViewModelToUserModel(RegistrationViewModel model) 
        {
            string directory = "C:\\Website\\Website\\wwwroot.registration.txt";
            using (StreamWriter sw = File.AppendText(directory))
            {
                string hostName = Dns.GetHostName();
                sw.WriteLine($"EMAIL: {model.Email!}, DATE: {DateTime.Now}, COMPUTER NAME: {hostName}, " +
                    $"IP: {Dns.GetHostEntry(hostName).AddressList[0]}");
            }
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!,
            };
        }
    }
}
