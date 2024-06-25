using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FeesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GetFees()
        {
            var fees = _unitOfWork.FeesAppService.GetFeesList().Where(x => x.Status == "Pending");

            return Json(new { data = fees });
        }


        [HttpGet]
        public IActionResult Index()
        {
            Navigation.ActivePage = "/Admin/Navigation/Index";
            return View();
        }

        //[HttpGet]
        //public IActionResult Review(int Id)
        //{

        //    var fee = _unitOfWork.FeesAppService.GetFeesList().Where(x => x.Id == Id).FirstOrDefault();

        //    return View(fee);

        //}

        [HttpGet("download-fee-voucher/{fileName}")]
        public IActionResult DownloadFeeVoucher(string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "FeeVouchers");
            var filePath = Path.Combine(path, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var mimeType = "application/pdf";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }


        [HttpGet]
        public async Task<IActionResult> PayFee(string voucher)
        {
            var response = false;
            var fee = _unitOfWork.FeesAppService.Get(x => x.FeeVoucher == voucher);

            fee.Status = "Paid";
            fee.PaidDate = DateTime.Now;

            _unitOfWork.FeesAppService.Update(fee);
            var result = await _unitOfWork.Save();

            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> RejectFee(string voucher)
        {
            var response = false;
            var fee = _unitOfWork.FeesAppService.Get(x => x.FeeVoucher == voucher);

            fee.Status = "Unpaid";

            if (fee.FeeVoucher != null)
            {
                var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, fee.FeeVoucher.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }
            fee.FeeVoucher = null;


            _unitOfWork.FeesAppService.Update(fee);
            var result = await _unitOfWork.Save();

            return Json(response);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties:"ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;
        //    return View();


        //}


        //[HttpPost]
        //public async Task<IActionResult> Create(Fees fe)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.FeesAppService.Add(fe);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Fees Added Successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            TempData["info"] = "Something Went Wrong";
        //            return View(fe);
        //        }
        //    }

        //    return View(fe);
        //}

        //[HttpGet]
        //public IActionResult Update(int id)
        //{
        //    var fe = _unitOfWork.FeesAppService.Get(x => x.Id == id);
        //    var studentList = _unitOfWork.StudentRegistrationAppService.GetAll(includeProperties: "ApplicationUser");
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (var item in studentList)
        //    {
        //        list.Add(new SelectListItem { Text = $"{item.ApplicationUser.FirstName} {item.ApplicationUser.LastName}", Value = item.Id.ToString() });


        //    }
        //    ViewBag.StudentRegistrationList = list;
        //    return View(fe);

        //}

        //[HttpPost]
        //public async Task<IActionResult> Update(Fees ss)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.FeesAppService.Update(ss);
        //        var result = await _unitOfWork.Save();
        //        if (result)
        //        {
        //            TempData["success"] = "Course Updated Successfully";
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

        //    var cource = _unitOfWork.FeesAppService.Get(x => x.Id == id);
        //    _unitOfWork.FeesAppService.Delete(cource);
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
