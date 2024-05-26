using CMS.CommonHelper;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers
{
    public class NavigationController : Controller
    {

        [HttpGet]
        [Route("/Navigation/Get")]
        public string Get()
        {
            string PageName = Navigation.GetActivePage();
            return PageName;
        }
    }
}
