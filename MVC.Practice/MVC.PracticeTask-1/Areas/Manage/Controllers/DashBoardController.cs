using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Member")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashBoardController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Creating Admin for the DashBoard
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    User user = new User()
        //    {
        //        FullName = "Elvin Sarkarov",
        //        UserName = "Admin1",
        //        BirthDate = "4 September",
        //    };

        //    var result = await _userManager.CreateAsync(user, "#Elvin123");

        //    return Ok(result);
        //}
        #endregion

        #region Creating Roles for Admin
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Editor");
        //    IdentityRole role4 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    await _roleManager.CreateAsync(role4);

        //    return Ok("Rollar Yarandi");
        //}
        #endregion

        #region Adding the role to spesific user
        //public async Task<IActionResult> AddRoleAdmin()
        //{
        //    User admin = await _userManager.FindByNameAsync("Admin1");

        //    await _userManager.AddToRoleAsync(admin, "Admin");

        //    return Ok("rol elave olundu");
        //}
        #endregion
    }
}
