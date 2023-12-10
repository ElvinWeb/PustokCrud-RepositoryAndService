using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewService
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutService(AppDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<List<Setting>> GetSetting()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            return settings;
        }
        public async Task<User> GetUser()
        {

            User user = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }

            return user;
        }

    }
}
