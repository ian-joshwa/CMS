using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult GetFees()
        {
            var studentRegistration = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");


            var fe = _unitOfWork.FeesAppService.GetAll();
            List<Fees> list = new List<Fees>();
            foreach(var fee in fe)
            {
                var reg = studentRegistration.Where(x => x.Id == fee.StudentRegistrationId).FirstOrDefault();
                fee.StudentRegistration = reg;
                list.Add(fee);
            }

            return Json(new { data = list });
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties:"ApplicationUser");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in studentList)
            {
                list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


            }
            ViewBag.StudentRegistrationList = list;
            return View();


        }


        [HttpPost]
        public async Task<IActionResult> Create(Fees fe)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.FeesAppService.Add(fe);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Fees Added Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(fe);
                }
            }

            return View(fe);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var fe = _unitOfWork.FeesAppService.Get(x => x.Id == id);
            var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in studentList)
            {
                list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


            }
            ViewBag.StudentRegistrationList = list;
            return View(fe);

        }

        [HttpPost]
        public async Task<IActionResult> Update(Fees ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.FeesAppService.Update(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Course Updated Successfully";
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

            var cource = _unitOfWork.FeesAppService.Get(x => x.Id == id);
            _unitOfWork.FeesAppService.Delete(cource);
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
