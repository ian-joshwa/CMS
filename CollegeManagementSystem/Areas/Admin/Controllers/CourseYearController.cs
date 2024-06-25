using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseYearController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseYearController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetCourseYears()
        {
            var courseYears = _unitOfWork.CourseYearAppService.GetAll();
            return Json(new { data = courseYears });
        }

        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/CourseYear/Index";
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseYear courseYear)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.CourseYearAppService.Add(courseYear);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Course Year Added Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(courseYear);
                }
            }

            return View(courseYear);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            var courseYear = _unitOfWork.CourseYearAppService.Get(x => x.Id == id);
            return View(courseYear);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseYear courseYear)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.CourseYearAppService.Update(courseYear);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Course Year Updated Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(courseYear);
                }
            }

            return View(courseYear);
        }


        public async Task<JsonResult> Delete(int id)
        {

            var courseYear = _unitOfWork.CourseYearAppService.Get(x => x.Id == id);
            _unitOfWork.CourseYearAppService.Delete(courseYear);
            var result = await _unitOfWork.Save();
            if (result)
            {
                return Json(new { result = result, message = "Course Year Deleted Successfully" });
            }
            else
            {
                return Json(new { result = result, message = "Something Went Wrong" });
            }

        }
    }
}
