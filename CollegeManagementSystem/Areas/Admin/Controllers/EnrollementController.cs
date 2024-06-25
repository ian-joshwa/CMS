using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollementController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetEnrollements()
        {
            var enrollementList = _unitOfWork.EnrollementAppService.GetEnrollementList().Include(x =>x.Session).Include(x => x.StudentRegistration).Include("StudentRegistration.ApplicationUser").ToList();
            return Json(new { data = enrollementList });
        }
        [HttpGet]
        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/Enrollement/Index";
            return View();
        }


        [HttpGet]
        public IActionResult Review(int Id)
        {

            var enrollment = _unitOfWork.EnrollementAppService.Get(x => x.Id == Id);

            var session = _unitOfWork.SessionAppService.Get(x => x.Id == enrollment.SessionId);
            var matric = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == enrollment.StudentRegistrationId && x.DocumentType == DocumentType.Matriculation);
            var inter = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == enrollment.StudentRegistrationId && x.DocumentType == DocumentType.Intermediate);

            ViewBag.Session = session;
            ViewBag.Matric = matric;
            ViewBag.Inter = inter;
            ViewBag.Enrollment = enrollment;

            return View();

        }


        [HttpGet]
        public async Task<IActionResult> StudentEnrollment(int Id)
        {

            var enrollment = _unitOfWork.EnrollementAppService.Get(x => x.Id == Id);
            enrollment.Status = "Enrolled";

            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.Id == enrollment.StudentRegistrationId);
            student.IsEnrolled = true;

            _unitOfWork.StudentRegistrationAppService.Update(student);
            _unitOfWork.EnrollementAppService.Update(enrollment);

            var session = _unitOfWork.SessionAppService.Get(x => x.Id == enrollment.SessionId);

            var startDate = session.StartTime;
            var endDate = session.EndTime;

            Fees AdmissionFee = new Fees();
            AdmissionFee.StudentRegistrationId = student.Id;
            AdmissionFee.Status = "Unpaid";
            AdmissionFee.FeeType = "Admission Fee";
            AdmissionFee.Amount = session.AdmissionFee;
            AdmissionFee.DueDate = startDate.AddDays(-1);

            _unitOfWork.FeesAppService.Add(AdmissionFee);

            while(startDate < endDate)
            {

                Fees MonthlyFee = new Fees();
                MonthlyFee.StudentRegistrationId = student.Id;
                MonthlyFee.Status = "Unpaid";
                MonthlyFee.FeeType = "Monthly Fee";
                MonthlyFee.Amount = session.MonthlyFee;
                MonthlyFee.DueDate = startDate.AddDays(-1);
                _unitOfWork.FeesAppService.Add(MonthlyFee);
                startDate = startDate.AddMonths(1);
            }



            var result = await _unitOfWork.Save();

            if (result)
            {

                TempData["success"] = "Student Enrolled Successfully";

            }
            else
            {
                TempData["error"] = "Something went wrong";
            }
            return RedirectToAction("Index");
        }



        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;

        //    var sessionList = _unitOfWork.SessionAppService.GetAll();
        //    List<SelectListItem> List = new List<SelectListItem>();
        //    foreach (var item in sessionList)
        //    {
        //        List.Add(new SelectListItem { Text = item.SessionName, Value = item.Id.ToString() });


        //    }
        //    ViewBag.Sessionlist = List;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Enrollement enrollement)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.EnrollementAppService.Add(enrollement);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Enrollement Added Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["info"] = "Something Went Wrong";
        //            return View(enrollement);
        //        }
        //    }

        //    return View(enrollement);
        //}
        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var fe = _unitOfWork.EnrollementAppService.Get(x => x.Id == id);
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;

        //    var sessionList = _unitOfWork.SessionAppService.GetAll();
        //    List<SelectListItem> List = new List<SelectListItem>();
        //    foreach (var item in sessionList)
        //    {
        //        List.Add(new SelectListItem { Text = item.SessionName, Value = item.Id.ToString() });


        //    }
        //    ViewBag.Sessionlist = List;
        //    var enrollement = _unitOfWork.EnrollementAppService.Get(x => x.Id == id);
        //    return View(enrollement);

        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(Enrollement enrollement)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.EnrollementAppService.Update(enrollement);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Enrollement Updated Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["info"] = "Something Went Wrong";
        //            return View(enrollement);
        //        }
        //    }

        //    return View(enrollement);
        //}


        //public async Task<JsonResult> Delete(int id)
        //{

        //    var ent= _unitOfWork.EnrollementAppService.Get(x => x.Id == id);
        //    _unitOfWork.EnrollementAppService.Delete(ent);
        //    var result = await _unitOfWork.Save();
        //    if (result)
        //    {
        //        return Json(new { result = result, message = "Data Deleted Successfully" });
        //    }
        //    else
        //    {
        //        return Json(new { result = result, message = "Something Went Wrong" });
        //    }

        //}
    }
    }

