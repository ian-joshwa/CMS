using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var ent = _unitOfWork.EnrollementAppService.GetAll();
            return Json(new { data = ent });
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
        public async Task<IActionResult> Create(Enrollement enrollement)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.EnrollementAppService.Add(enrollement);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Enrollement Added Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(enrollement);
                }
            }

            return View(enrollement);
        }
        [HttpGet]
        //public IActionResult Update(int id)
        //{

        //    var cource = _unitOfWork.EnrollementAppService.Get(x => x.Id == id);
        //    //var sessionList = _unitOfWork.SessionAppService.GetAll();
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in sessionList)
        //    {
        //        list.Add(new SelectListItem { Text = item.SessionName, Value = item.Id.ToString() });
        //    }
        //    ViewBag.SessionList = list;
        //    return View(cource);

        //}

        [HttpPost]
        public async Task<IActionResult> Update(Enrollement enrollement)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.EnrollementAppService.Update(enrollement);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Enrollement Updated Successfully";
                    return View("Index");
                }
                else
                {
                    TempData["info"] = "Something Went Wrong";
                    return View(enrollement);
                }
            }

            return View(enrollement);
        }


        public async Task<JsonResult> Delete(int id)
        {

            var ent= _unitOfWork.EnrollementAppService.Get(x => x.Id == id);
            _unitOfWork.EnrollementAppService.Delete(ent);
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

