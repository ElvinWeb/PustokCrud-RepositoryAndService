using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ChatController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }
    }
}
