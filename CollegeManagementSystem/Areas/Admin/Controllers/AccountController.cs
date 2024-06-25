using CMS.CommonHelper;
using CMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var user = await _userManager.GetUserAsync(User);
            Navigation.ActivePage = "/Admin/Account/Profile";
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
                    Navigation.ActivePage = "/Admin/Account/Profile";
                    return View(usr);
                }
                else
                {
                    TempData["success"] = "Profile Updated";
                    Navigation.ActivePage = "/Admin/Account/Profile";
                    return RedirectToAction("Profile");
                }

            }
            else
            {
                TempData["error"] = "Something went wrong";
                Navigation.ActivePage = "/Admin/Account/Profile";
                return View(usr);
            }

        }


        [HttpGet]
        public IActionResult ChangePassword()
        {
            Navigation.ActivePage = "/Admin/Account/ChangePassword";
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
                    Navigation.ActivePage = "/Admin/Account/ChangePassword";
                    return View(password);
                }
                else
                {
                    TempData["success"] = "Password Updated";
                    await _signInManager.RefreshSignInAsync(user);
                    Navigation.ActivePage = "/Admin/Account/ChangePassword";
                    return RedirectToAction("ChangePassword");
                }

            }
            else
            {
                TempData["error"] = "Something went wrong";
                Navigation.ActivePage = "/Admin/Account/ChangePassword";
                return View(password);
            }

        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(AdminRegister model)
        {

            if (ModelState.IsValid)
            {

                var user = CreateUser();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, WebsiteRoles.Role_Admin);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/Admin/Home");
                }
                else
                {
                    return View(model);
                }

            }
            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect("/");
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

    }
}
