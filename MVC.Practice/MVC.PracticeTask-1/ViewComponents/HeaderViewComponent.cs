using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.Services.Implementations;
using MVC.PracticeTask_1.ViewModel;
using NuGet.Configuration;

namespace MVC.PracticeTask_1.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IGenreService _genreService;
        private readonly AppDbContext _context;
        public HeaderViewComponent(IGenreService genreService, AppDbContext context)
        {
            _context = context;
            _genreService = genreService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel headerViewModel = new HeaderViewModel()
            {
                Genres = await _genreService.GetAllAsync(),
                Settings = await _context.Settings.ToListAsync(),
            };

            return View(headerViewModel);
        }
    }
}
