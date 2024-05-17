using CMS.CommonHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        [Route("/Admin/Home")]
        public IActionResult Home()
        {

            return View();
        }
    }
}
