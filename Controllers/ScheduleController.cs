using Microsoft.AspNetCore.Mvc;
using Website.ViewModels;

namespace Website.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpGet]
        [Route("/schedule")]
        public async Task<IActionResult> Schedule(ScheduleViewModel model)
        {
            return await Task.FromResult(View("Index", model));
        }
    }
}
