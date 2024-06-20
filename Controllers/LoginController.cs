using Microsoft.AspNetCore.Mvc;
using System.IO;
using Website.BL.Auth;
using Website.ViewMapper;
using Website.ViewModels;

namespace Website.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBL;
        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBL.Authentificate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (Website.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }
            return View("Index", model);
        }
    }
}
