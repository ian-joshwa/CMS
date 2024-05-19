using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public InstructorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult GetInstructors()
        {
            var inst = _unitOfWork.InstructorAppService.GetAll(includeProperties: "Department");
            return Json(new { data = inst });
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            var deptList = _unitOfWork.DepartmentAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in deptList)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Departmentlist = list;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Instructor dd)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.InstructorAppService.Add(dd);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = " Instructor  Added Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(dd);
                }
            }

            return View(dd);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var instructor = _unitOfWork.InstructorAppService.Get(x => x.Id == id);
            var deptlist = _unitOfWork.DepartmentAppService.GetAll();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in deptlist)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.Departmentlist = list;
            return View(instructor);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Instructor dd)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.InstructorAppService.Update(dd);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Instructor Updated Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(dd);
                }
            }

            return View(dd);
        }


        public async Task<JsonResult> Delete(int id)
        {

            var dept = _unitOfWork.InstructorAppService.Get(x => x.Id == id);
            _unitOfWork.InstructorAppService.Delete(dept);
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
