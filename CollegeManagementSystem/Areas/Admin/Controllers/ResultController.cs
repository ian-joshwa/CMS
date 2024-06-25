using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResultController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResultController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult GetResults()
        {
            var resultList = _unitOfWork.ResultAppService.GetResultList().Include(x => x.Course).Include(x => x.StudentRegistration).Include("StudentRegistration.ApplicationUser").ToList();
            return Json(new { data = resultList });
        }

        public IActionResult GetSessions()
        {

            var sessions = _unitOfWork.SessionAppService.GetAll().Where(x => x.Status == SessionStatus.Ongoing).ToList();
            
            return Json( new { data = sessions });

        }

        [HttpGet]
        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/Result/Index";
            return View();
        }
        [HttpGet]
        public IActionResult Session(int Id)
        {

            var session = _unitOfWork.SessionAppService.Get(x => x.Id == Id);
            var enrollments = _unitOfWork.EnrollementAppService.GetAll().Where(x => x.SessionId == session.Id).ToList();

            var students = new List<StudentRegistration>();

            foreach(var enroll in enrollments)
            {

                var student = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser").Where(x => x.Id == enroll.StudentRegistrationId).FirstOrDefault();
                students.Add(student);

            }

            return View(students);


        }


        [HttpGet]
        public IActionResult Courses(int Id)
        {

            var enrollment = _unitOfWork.EnrollementAppService.Get(x => x.StudentRegistrationId == Id);

            var courses = _unitOfWork.CourseAppService.GetAll(includeProperties: "CourseYear").Where(x => x.SessionId == enrollment.SessionId).OrderBy(x => x.CourseYear.Year).ToList();

            ViewBag.Student = Id;
            return View(courses);
        }
        
        
        [HttpGet]
        public IActionResult Add(int StudentId, int Course)
        {

            var getResult = _unitOfWork.ResultAppService.Get(x => x.CourseId == Course && x.StudentRegistrationId == StudentId, includeProperties:"Course");


            if(getResult != null)
            {
                return View(getResult);
            }

            Result result = new Result();
            result.StudentRegistrationId = StudentId;
            result.CourseId = Course;
            var cs = _unitOfWork.CourseAppService.Get(x => x.Id == Course,includeProperties: "CourseYear");
            result.Course = cs;
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Result rst)
        {

            if(rst.Id > 0)
            {

                _unitOfWork.ResultAppService.Update(rst);

            }
            else
            {
                _unitOfWork.ResultAppService.Add(rst);
            }
            var result = await _unitOfWork.Save();
            if (result)
            {
                TempData["Success"] = "Result Updated Successfully";
            }
            else
            {
                TempData["error"] = "Something Went Wrong";
            }
            return RedirectToAction("Courses", new { Id = rst.StudentRegistrationId });
        }



        //[HttpGet]
        //public IActionResult Create() {
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;

        //    var examlist = _unitOfWork.ExaminationAppService.GetAll();
        //    List<SelectListItem> examList = new List<SelectListItem>();
        //    foreach (var item in examlist)
        //    {
        //        examList.Add(new SelectListItem { Text = item.ExamName, Value = item.Id.ToString() });


        //    }
        //    ViewBag.ExaminationList = examList;
        //    return View();


        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Result rr)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.ResultAppService.Add(rr);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Result Added Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["info"] = "Something Went Wrong";
        //            return View(rr);
        //        }
        //    }

        //    return View(rr);
        //}

        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var fe = _unitOfWork.ResultAppService.Get(x => x.Id == id);
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;

        //    var examlist = _unitOfWork.ExaminationAppService.GetAll();
        //    List<SelectListItem> exmlt = new List<SelectListItem>();
        //    foreach (var item in examlist)
        //    {
        //        exmlt.Add(new SelectListItem { Text = item.ExamName, Value = item.Id.ToString() });


        //    }
        //    ViewBag.ExaminationList = exmlt;
        //    var result = _unitOfWork.ResultAppService.Get(x => x.Id == id);
        //    return View(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(Result ss)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.ResultAppService.Update(ss);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Result Updated Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["info"] = "Something Went Wrong";
        //            return View(ss);
        //        }
        //    }

        //    return View(ss);
        //}


        //public async Task<JsonResult> Delete(int id)
        //{

        //    var res= _unitOfWork.ResultAppService.Get(x => x.Id == id);
        //    _unitOfWork.ResultAppService.Delete(res);
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
