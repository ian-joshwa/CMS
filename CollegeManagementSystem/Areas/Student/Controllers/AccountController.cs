using CMS.CommonHelper;
using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webRoot, 
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _webHostEnvironment = webRoot;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var user = await _userManager.GetUserAsync(User);
            Navigation.ActivePage = "/Student/Account/Profile";
            return View(user);

        }


        [HttpPost]
        public async Task<IActionResult> Profile(ApplicationUser usr, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (file != null)
                {
                    if (user.ProfilePic != null)
                    {
                        var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePic.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImages");
                    var filename = Guid.NewGuid().ToString() + "-" + file.FileName;
                    var filepath = Path.Combine(path, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    user.ProfilePic = @"\ProfileImages\" + filename;

                }

                user.FirstName = usr.FirstName;
                user.LastName = usr.LastName;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    TempData["error"] = "Something went wrong";
                    Navigation.ActivePage = "/Student/Account/Profile";
                    return View(usr);
                }
                else
                {
                    TempData["success"] = "Profile Updated";
                    Navigation.ActivePage = "/Student/Account/Profile";
                    return RedirectToAction("Profile");
                }

            }
            else
            {
                TempData["error"] = "Something went wrong";
                Navigation.ActivePage = "/Student/Account/Profile";
                return View(usr);
            }

        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            Navigation.ActivePage = "/Student/Account/ChangePassword";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(Password password)
        {

            if (ModelState.IsValid)
            {

                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.ChangePasswordAsync(user, password.CurrentPassword, password.NewPassword);

                if (!result.Succeeded)
                {
                    TempData["error"] = "Something went wrong";
                    Navigation.ActivePage = "/Student/Account/ChangePassword";
                    return View(password);
                }
                else
                {
                    TempData["success"] = "Password Updated";
                    await _signInManager.RefreshSignInAsync(user);
                    Navigation.ActivePage = "/Student/Account/ChangePassword";
                    return RedirectToAction("ChangePassword");
                }

            }
            else
            {
                TempData["error"] = "Something went wrong";
                Navigation.ActivePage = "/Student/Account/ChangePassword";
                return View(password);
            }

        }


    }
}
