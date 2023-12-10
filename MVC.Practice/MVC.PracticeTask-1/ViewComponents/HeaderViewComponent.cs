using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.Services.Implementations;
using MVC.PracticeTask_1.ViewModel;
using NuGet.Configuration;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HeaderViewComponent(IGenreService genreService, AppDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _genreService = genreService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            User user = null;

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }

            HeaderViewModel headerViewModel = new HeaderViewModel()
            {
                Genres = await _genreService.GetAllAsync(),
                Settings = await _context.Settings.ToListAsync(),
                User = user
            };

            return View(headerViewModel);
        }
    }
}
