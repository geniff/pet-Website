using Microsoft.AspNetCore.Mvc;
using Website.Middleware;
using Website.ViewModels;
using Website.Service;

namespace Website.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave()
        {
            //if (ModelState.IsValid())
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                WebFileService webfile = new WebFileService();
                string filename = webfile.GetWebFileName(imageData.FileName);
                await webfile.UploadAndResizeImage(imageData.OpenReadStream(), filename, 800, 600);
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
