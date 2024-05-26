using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var resultList = _unitOfWork.ResultAppService.GetResultList().Include(x => x.Examination).Include(x => x.StudentRegistration).Include("StudentRegistration.ApplicationUser").ToList();
            return Json(new { data = resultList });
        }
        [HttpGet]
        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/Result/Index";
            return View();
        }
        

        [HttpGet]
        public IActionResult Create() {
            var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in studentList)
            {
                list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


            }
            ViewBag.StudentRegistrationList = list;

            var examlist = _unitOfWork.ExaminationAppService.GetAll();
            List<SelectListItem> examList = new List<SelectListItem>();
            foreach (var item in examlist)
            {
                examList.Add(new SelectListItem { Text = item.ExamName, Value = item.Id.ToString() });


            }
            ViewBag.ExaminationList = examList;
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Create(Result rr)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.ResultAppService.Add(rr);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Result Added Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(rr);
                }
            }

            return View(rr);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var fe = _unitOfWork.ResultAppService.Get(x => x.Id == id);
            var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in studentList)
            {
                list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


            }
            ViewBag.StudentRegistrationList = list;

            var examlist = _unitOfWork.ExaminationAppService.GetAll();
            List<SelectListItem> exmlt = new List<SelectListItem>();
            foreach (var item in examlist)
            {
                exmlt.Add(new SelectListItem { Text = item.ExamName, Value = item.Id.ToString() });


            }
            ViewBag.ExaminationList = exmlt;
            var result = _unitOfWork.ResultAppService.Get(x => x.Id == id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Result ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.ResultAppService.Update(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Result Updated Successfully";
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

            var res= _unitOfWork.ResultAppService.Get(x => x.Id == id);
            _unitOfWork.ResultAppService.Delete(res);
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
