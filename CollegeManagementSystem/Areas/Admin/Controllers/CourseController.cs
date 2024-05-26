using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetCourses()
        {
            var couces = _unitOfWork.CourseAppService.GetAll(includeProperties: "Session");
            return Json(new { data = couces });
        }


        [HttpGet]
        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/Course/Index";
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var  sessionList=  _unitOfWork.SessionAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in sessionList)
            {
                list.Add(new SelectListItem { Text = item.SessionName, Value = item.Id.ToString() });
               

            }
            ViewBag.Sessionlist = list;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Course cc)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CourseAppService.Add(cc);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Course Added Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(cc);
                }
            }

            return View(cc);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            var cource = _unitOfWork.CourseAppService.Get(x => x.Id == id);
            var sessionList = _unitOfWork.SessionAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in sessionList)
            {
                list.Add(new SelectListItem { Text = item.SessionName, Value = item.Id.ToString() });
            }
            ViewBag.SessionList = list;
            return View(cource);
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.CourseAppService.Update(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Course Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(ss);
                }
            }

            return View(ss);
        }


        public async Task<JsonResult> Delete(int id)
        {

            var cource = _unitOfWork.CourseAppService.Get(x => x.Id == id);
            _unitOfWork.CourseAppService.Delete(cource);
            var result = await _unitOfWork.Save();
            if (result)
            {
                return Json(new { result = result, message = "Data Deleted Successfully" });
            }
            else
            {
                return Json(new { result = result, message = "Something Went Wrong" });
            }

        }


    }
}
   
