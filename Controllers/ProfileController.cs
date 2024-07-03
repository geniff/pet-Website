using Microsoft.AspNetCore.Mvc;
using Website.Middleware;
using Website.ViewModels;
using Website.Service;
using Website.BL.Auth;
using Website.BL.Profile;
using Website.ViewMapper;
using Website.DAL.Models;
using Resunet.ViewMapper;

namespace Website.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;
        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            this.currentUser = currentUser;
            this.profile = profile;
        }
        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profiles = await currentUser.GetProfiles();
            ProfileModel? profileModel = profiles.FirstOrDefault();

            return View(profileModel != null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile/uploadimage")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> ImageSave(int? profileId)
        {
            int? userid = await currentUser.GetCurrentUserId();

            if (userid == null)
            {
                throw new ArgumentNullException("Пользователь не найден");
            }

            var profiles = await profile.Get((int)userid);

            if (profileId != null && !profiles.Any(m => m.ProfileId == profileId))
            {
                throw new Exception("Error");
            }

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = profiles.FirstOrDefault(m => m.ProfileId == profileId) ?? new ProfileModel();
                profileModel.UserId = (int)userid;
                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    WebFileService webfile = new WebFileService();
                    string filename = webfile.GetWebFileName(Request.Form.Files[0].FileName);
                    await webfile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
                    profileModel.ProfileImage = filename;
                    await profile.AddOrUpdate(profileModel);
                }
            }

            return Redirect("/profile");
        }
        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? userid = await currentUser.GetCurrentUserId();

            if (userid == null)
            {
                throw new ArgumentNullException("Пользователь не найден");
            }

            var profiles = await profile.Get((int)userid);

            if (model.ProfileId != null && !profiles.Any(m => m.ProfileId == model.ProfileId))
            {
                throw new Exception("Error");
            }

            if (ModelState.IsValid)
            {
                ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
                profileModel.UserId = (int)userid;
                await profile.AddOrUpdate(profileModel);
                return Redirect("/");
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
