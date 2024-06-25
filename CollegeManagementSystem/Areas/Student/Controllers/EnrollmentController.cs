using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class EnrollmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public JsonResult GetSessionSeats(int id)
        {

            var enrolled = _unitOfWork.EnrollementAppService.GetAll().Where(x => x.SessionId == id && x.Status == "Enrolled").ToList();
            var session = _unitOfWork.SessionAppService.Get(x => x.Id == id);

            var seats = session.Capacity - enrolled.Count;
            return Json(seats);
        }

        [HttpGet]
        public IActionResult Enrollment()
        {

            var sessions = _unitOfWork.SessionAppService.GetAll().Where(x => x.Status == SessionStatus.UpComing).ToList();
            Navigation.ActivePage = "/Student/Enrollment/Enrollment";
            return View(sessions);
        }

        [HttpGet]
        public async Task<IActionResult> SessionDetails(int id)
        {

            var session = _unitOfWork.SessionAppService.Get(x => x.Id == id);
            var courses = _unitOfWork.CourseAppService.GetAll(includeProperties: "CourseYear").Where(x => x.SessionId == id);
            courses = courses.OrderBy(x => x.CourseYear.Year).ToList(); 
            ViewBag.Session = session;
            ViewBag.Courses = courses;

            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var enroll = _unitOfWork.EnrollementAppService.Get(x => x.StudentRegistrationId == student.Id);

            if(enroll == null)
            {
                ViewBag.IsEnrolled = false;
            }
            else
            {
                ViewBag.IsEnrolled = true;
            }

            var matric = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == student.Id && x.DocumentType == DocumentType.Matriculation);
            var inter = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == student.Id && x.DocumentType == DocumentType.Intermediate);

            if (session.IsInterDocumentRequired)
            {

                if(matric == null)
                {
                    TempData["info"] = "Matriculation Document Required";
                    ViewBag.Eligible = "Matriculation Document Required";
                    ViewBag.IsEligible = false;
                    return View();
                }

                if (inter == null)
                {
                    TempData["info"] = "Intermediate Document Required";
                    ViewBag.Eligible = "Intermediate Document Required";
                    ViewBag.IsEligible = false;
                    return View();
                }

            }

            float merit;

            if (session.IsInterDocumentRequired)
            {
                merit = ((float)matric.ObtainedMarks / (float)matric.TotalMarks * 30) + ((float)inter.ObtainedMarks / (float)inter.TotalMarks * 70);
            }
            else
            {
                if(matric == null)
                {
                    ViewBag.Eligible = "Matriculation Document Required";
                    ViewBag.IsEligible = false;
                    return View();
                }
                else
                {
                    merit = ((float)matric.ObtainedMarks / (float)matric.TotalMarks * 100);
                }
            }

            if(merit > session.Merit)
            {
                ViewBag.Eligible = "Eligible";
                
            }
            else
            {
                ViewBag.Eligible = "Not Eligible";
                
            }

            var enrolled = _unitOfWork.EnrollementAppService.GetAll().Where(x => x.SessionId == id).ToList();

            var seats = session.Capacity - enrolled.Count;

            if(merit > session.Merit && seats > 0)
            {
                ViewBag.IsEligible = true;
            }
            else
            {
                ViewBag.IsEligible = false;
            }


            return View();
        }


        [HttpGet]
        public async Task<IActionResult> StudentEnroll(int Id)
        {

            var user = await _userManager.GetUserAsync(User);
            var std = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            Enrollement enrollment = new Enrollement();

            enrollment.StudentRegistrationId = std.Id;
            enrollment.SessionId = Id;
            enrollment.Status = "Pending";

            _unitOfWork.EnrollementAppService.Add(enrollment);
            var result = await _unitOfWork.Save();
            if (result)
            {

                TempData["success"] = "Enrollment Request Generated";
                TempData["info"] = "Request Underview";

            }
            else
            {
                TempData["error"] = "Something went wrong";
            }

            return RedirectToAction("Enrollment");
        }


    }
}
