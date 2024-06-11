using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class EnrollmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Enrollment()
        {

            var sessions = _unitOfWork.SessionAppService.GetAll().Where(x => x.Status == SessionStatus.Pending).ToList();
            Navigation.ActivePage = "/Student/Enrollment/Enrollment";
            return View(sessions);
        }
    }
}
