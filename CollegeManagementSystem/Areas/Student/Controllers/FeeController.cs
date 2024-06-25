using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class FeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FeeController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var fees = _unitOfWork.FeesAppService.GetFeesList().Where(x => x.StudentRegistrationId == student.Id).ToList();


            ViewBag.FeeList = fees;
            Navigation.ActivePage = "/Student/Fee/Index";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PayFee(int id)
        {

            //var user = await _userManager.GetUserAsync(User);
            //var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var fee = _unitOfWork.FeesAppService.Get(x => x.Id == id);
            return View(fee);

        }
        
        
        [HttpPost]
        public async Task<IActionResult> PayFee(Fees FEE, IFormFile? file)
        {

            
            var Fees = _unitOfWork.FeesAppService.Get(x => x.Id == FEE.Id);
            if (file != null)
            {

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "FeeVouchers");
                var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                var filepath = Path.Combine(path, filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Fees.FeeVoucher = filename;

            }
            Fees.Status = "Pending";
            _unitOfWork.FeesAppService.Update(Fees);
           await _unitOfWork.Save();

            TempData["info"] = "Fees Payment Request Generated";

            return RedirectToAction("Index");

        }


    }
}
