using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Website.BL.AboutOfStudent;
using Website.BL.Auth;
using Website.ViewModels;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICurrentUser currentUser;
        private readonly IAboutOfStudent aboutOfStudent;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IAboutOfStudent aboutOfStudent)
        {
            this.logger = logger;
            this.currentUser = currentUser;
            this.aboutOfStudent = aboutOfStudent;
        }

        public async Task <IActionResult> Index()
        {
            var latestAboutOfStudents = await aboutOfStudent.Search(4);
            return View(latestAboutOfStudents);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
