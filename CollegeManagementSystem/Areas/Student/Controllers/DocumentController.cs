using CMS.CommonHelper;
using CMS.DataAccessLayer.Infrastructure.Interfaces;
using CMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class DocumentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DocumentController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);

            var inter = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == student.Id && x.DocumentType == DocumentType.Intermediate);
            var matric = _unitOfWork.StudentDocumentAppService.Get(x => x.StudentId == student.Id && x.DocumentType == DocumentType.Matriculation);
            
            var IsInter = false;
            var IsMatric = false;

            if(inter != null && inter.Id > 0)
            {
                IsInter = true;
                ViewBag.Inter = inter;
            }
            else
            {
                ViewBag.Inter = new StudentDocument();
            }

            if(matric != null && matric.Id > 0)
            {
                IsMatric = true;
                ViewBag.Matric = matric;
            }
            else
            {
                ViewBag.Matric = new StudentDocument();
            }

            ViewBag.IsInter = IsInter;
            ViewBag.IsMatric = IsMatric;

            Navigation.ActivePage = "/Student/Document/Index";
            return View();
        }


        public IActionResult Matriculation(int Id)
        {

            ViewBag.Combination = CombinationMatric.GetCombinations();
            var document = new StudentDocument();
            if(Id > 0)
            {
                document = _unitOfWork.StudentDocumentAppService.Get(x => x.Id == Id);
            }
            
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> Matriculation(StudentDocument studentDocument, IFormFile? file)
        {

            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);
            studentDocument.StudentId = student.Id;
            studentDocument.DocumentType = DocumentType.Matriculation;
            if (studentDocument.Id > 0)
            {

                var document = _unitOfWork.StudentDocumentAppService.Get(x => x.Id == studentDocument.Id);
                if (file != null)
                {
                    if (document.Document != null)
                    {
                        var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, document.Document.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentImages");
                    var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filepath = Path.Combine(path, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    studentDocument.Document = @"\DocumentImages\" + filename;

                }

                if(file == null && document.Document != null)
                {
                    studentDocument.Document = document.Document;
                }

                _unitOfWork.StudentDocumentAppService.Update(studentDocument);
                
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Document Saved Successfully";
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                }
            }
            else
            {

                if(file != null)
                {

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentImages");
                    var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filepath = Path.Combine(path, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    studentDocument.Document = @"\DocumentImages\" + filename;

                }
                
                _unitOfWork.StudentDocumentAppService.Add(studentDocument);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Document Updated Successfully";
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                }
            }

            return RedirectToAction("Index");

        }



        public IActionResult Intermediate(int Id)
        {

            ViewBag.Combination = CombinationInter.GetCombinations();
            var document = new StudentDocument();
            if (Id > 0)
            {
                document = _unitOfWork.StudentDocumentAppService.Get(x => x.Id == Id);
            }

            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> Intermediate(StudentDocument studentDocument, IFormFile? file)
        {

            var user = await _userManager.GetUserAsync(User);
            var student = _unitOfWork.StudentRegistrationAppService.Get(x => x.ApplicationUserId == user.Id);
            studentDocument.StudentId = student.Id;
            studentDocument.DocumentType = DocumentType.Intermediate;
            if (studentDocument.Id > 0)
            {

                var document = _unitOfWork.StudentDocumentAppService.Get(x => x.Id == studentDocument.Id);
                if (file != null)
                {
                    if (document.Document != null)
                    {
                        var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, document.Document.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentImages");
                    var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filepath = Path.Combine(path, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    studentDocument.Document = @"\DocumentImages\" + filename;

                }

                if (file == null && document.Document != null)
                {
                    studentDocument.Document = document.Document;
                }

                _unitOfWork.StudentDocumentAppService.Update(studentDocument);

                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Document Saved Successfully";
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                }
            }
            else
            {

                if (file != null)
                {

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "DocumentImages");
                    var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filepath = Path.Combine(path, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    studentDocument.Document = @"\DocumentImages\" + filename;

                }

                _unitOfWork.StudentDocumentAppService.Add(studentDocument);
                var result = await _unitOfWork.Save();
                if (result)
                {
                    TempData["success"] = "Document Updated Successfully";
                }
                else
                {
                    TempData["error"] = "Something went wrong";
                }
            }

            return RedirectToAction("Index");

        }






    }
}
