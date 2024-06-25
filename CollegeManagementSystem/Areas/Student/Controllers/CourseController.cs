using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class CourseController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public CourseController(IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var std = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var enrollment = _unitOfWork.EnrollementAppService.Get(x => x.StudentRegistrationId == std.Id);
            var session = _unitOfWork.SessionAppService.Get(x => x.Id == enrollment.SessionId);

            var courses = _unitOfWork.CourseAppService.GetAll(includeProperties:"CourseYear").Where(x => x.SessionId == session.Id).ToList();

            ViewBag.Courses = courses;
            Navigation.ActivePage = "/Student/Course/Index";
            return View();
        }
    }
}
