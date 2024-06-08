using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public JsonResult GetStudentCount()
        {

            var students = _unitOfWork.StudentRegistrationAppService.GetAll().ToList(); 
            return Json(students.Count);

        }


        [Route("/Admin/Home")]
        public IActionResult Home()
        {
            Navigation.ActivePage = "/Admin/Home/Home";
            return View();
        }
    }
}
