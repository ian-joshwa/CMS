using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class ResultController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ResultController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }



        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var resultList = _unitOfWork.ResultAppService.GetAll(includeProperties: "Course,Course.CourseYear").Where(x => x.StudentRegistrationId == student.Id).OrderBy(x => x.Course.CourseYear.Year).ToList();
            Navigation.ActivePage = "/Student/Result/Index";
            return View(resultList);
        }
    }
}
