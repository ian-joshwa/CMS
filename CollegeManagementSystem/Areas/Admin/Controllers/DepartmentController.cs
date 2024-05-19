using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult GetDepartments()
        {
            var depts = _unitOfWork.DepartmentAppService.GetAll();
            return Json(new { data = depts });
        }



        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dd)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.DepartmentAppService.Add(dd);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = " Department  Added Successfully";
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

            var department = _unitOfWork.DepartmentAppService.Get(x => x.Id == id);
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department dd)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.DepartmentAppService.Update(dd);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Session Updated Successfully";
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

            var dept = _unitOfWork.DepartmentAppService.Get(x => x.Id == id);
            _unitOfWork.DepartmentAppService.Delete(dept);
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
