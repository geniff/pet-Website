using System.Net;
using We.BL.Auth;
using Website.BL.Auth;
using Website.BL.AboutOfStudent;
using Website.DAL;

namespace Website
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<Website.DAL.IDbSessionDAL, Website.DAL.DbSessionDAL>();
            builder.Services.AddSingleton<Website.DAL.IAuthDAL, Website.DAL.AuthDAL>();
            builder.Services.AddSingleton<Website.DAL.IUserTokenDAL, Website.DAL.UserTokenDAL>();
            builder.Services.AddSingleton<Website.DAL.IProfileDAL, Website.DAL.ProfileDAL>();

            builder.Services.AddScoped<Website.BL.Auth.IAuth, Website.BL.Auth.Auth>();
            builder.Services.AddSingleton<Website.BL.Auth.IEncrypt, Website.BL.Auth.Encrypt>();
            builder.Services.AddScoped<Website.BL.Auth.ICurrentUser, Website.BL.Auth.CurrentUser>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<Website.BL.Auth.IDbSession, Website.BL.Auth.DbSession>();
            builder.Services.AddScoped<Website.BL.General.IWebCookie, Website.BL.General.WebCookie>();
            builder.Services.AddSingleton<Website.BL.Profile.IProfile, Website.BL.Profile.Profile>();
            builder.Services.AddSingleton<Website.BL.AboutOfStudent.IAboutOfStudent, Website.BL.AboutOfStudent.AboutOfStudent>();

            builder.Services.AddMvc();

            builder.Services.AddSingleton<ICaptcha>(x => new GoogleCaptcha(builder.Configuration["Captcha:SiteKey"] ?? "", builder.Configuration["Captcha:SecretKey"] ?? ""));

            var app = builder.Build();

            if (!builder.Environment.IsDevelopment())
            {
                builder.Services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}