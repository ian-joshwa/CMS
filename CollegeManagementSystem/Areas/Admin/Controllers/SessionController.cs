using CMS.DataAccessLayer.Data;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Collections.Immutable;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SessionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SessionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult GetSessions()
        {
            var sessions = _unitOfWork.SessionAppService.GetAll();
            return Json(new {data = sessions });
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
        public async Task<IActionResult> Create(Session ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.SessionAppService.Add(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Session Added Successfully";
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

        [HttpGet]
        public IActionResult Update(int id)
        {

            var session = _unitOfWork.SessionAppService.Get(x => x.SessionId == id);
            return View(session);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Session ss)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.SessionAppService.Update(ss);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Session Updated Successfully";
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

            var session = _unitOfWork.SessionAppService.Get(x => x.SessionId == id);
            _unitOfWork.SessionAppService.Delete(session);
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
