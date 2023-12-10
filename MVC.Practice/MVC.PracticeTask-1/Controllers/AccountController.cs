using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.ViewModel;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
        {
            if (!ModelState.IsValid) return View();
            User user = null;

            user = await _userManager.FindByNameAsync(memberRegisterVM.Username);

            if (user != null)
            {
                ModelState.AddModelError("Username", "username has already exists!");
                return View();
            }
            user = await _userManager.FindByNameAsync(memberRegisterVM.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", "email has already exists!");
                return View();
            }

            User appUser = new User()
            {
                FullName = memberRegisterVM.Fullname,
                UserName = memberRegisterVM.Username,
                Email = memberRegisterVM.Email,
                BirthDate = memberRegisterVM.BirthDate,

            };
            var result = await _userManager.CreateAsync(appUser, memberRegisterVM.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View(err);
                }
            }


            await _userManager.AddToRoleAsync(appUser, "Member");

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            if (!ModelState.IsValid) return View();
            User user = null;


            user = await _userManager.FindByNameAsync(memberLoginVM.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Error!");
                return View();
            }

            user = await _userManager.FindByEmailAsync(memberLoginVM.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Error!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Fill the inputs in correct manner!!");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Register", "Account");
        }
    }
}
