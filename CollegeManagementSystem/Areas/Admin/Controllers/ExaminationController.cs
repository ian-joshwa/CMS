using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ExaminationController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;

        public ExaminationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult GetExaminations()
        {
            var sessions = _unitOfWork.ExaminationAppService.GetAll(includeProperties: "Course");
            return Json(new { data = sessions });
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create() {

            var courseList = _unitOfWork.CourseAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in courseList)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });


            }
            ViewBag.CourseList = list;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Examination ex)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.ExaminationAppService.Add(ex);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Exam Added Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(ex);
                }
            }

            return View(ex);




        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            var cource = _unitOfWork.ExaminationAppService.Get(x => x.Id == id);
            var courseList = _unitOfWork.CourseAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in courseList)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.CourseList = list;
            return View(cource);

        }

        [HttpPost]
        public async Task<IActionResult> Update(Examination ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.ExaminationAppService.Update(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Exam Updated Successfully";
                    return View("Index");
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

            var cource = _unitOfWork.ExaminationAppService.Get(x => x.Id == id);
            _unitOfWork.ExaminationAppService.Delete(cource);
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
