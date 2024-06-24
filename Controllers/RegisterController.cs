using System;
using Website.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Website.ViewModels;
using Website.ViewMapper;
using Website.BL;
using Website.Middleware;
namespace Website.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController: Controller
    {
        private readonly IAuth authBL;
        private readonly ICaptcha captcha;
        public RegisterController(IAuth authBL, ICaptcha captcha)
        {
            this.authBL = authBL;
            this.captcha = captcha;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            ViewBag.CaptchaSitekey = captcha.GetSitekey();
            RegistrationViewModel model = new RegistrationViewModel();
            return View("Index", new RegistrationViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegistrationViewModel model)
        {
            ViewBag.CaptchaSitekey = captcha.GetSitekey();
            bool isCaptchaValid = await captcha.ValidateToken(Request.Form["g-recaptcha-response"]!);
            if (isCaptchaValid)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await authBL.CreateUser(AuthMapper.MapRegistrationViewModelToUserModel(model));
                        return Redirect("/");
                    }
                    catch (DuplicateEmailException) 
                    {
                        ModelState.TryAddModelError("Email", "Email уже существует");
                    }
                }
            }
            else
            {
                ModelState.TryAddModelError("captcha", "Incorrect Captcha");
            }
            return View("Index", model);
        }
    }
}
