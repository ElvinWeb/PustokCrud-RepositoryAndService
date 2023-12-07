using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly IBookService _bookService;
        private readonly AppDbContext _dbContext;

        private AppViewModel _ViewModel = new AppViewModel();
        public HomeController(ISliderService sliderService, IBookService bookService, AppDbContext dbContext)
        {

            _sliderService = sliderService;
            _bookService = bookService;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            _ViewModel.Slides = await _sliderService.GetAllAsync();
            _ViewModel.Services = _dbContext.Services.ToList();
            _ViewModel.NewBooks = await _bookService.GetNewBooksAsync();
            _ViewModel.FeaturedBooks = await _bookService.GetFeaturedBooksAsync();
            _ViewModel.BestsellerBooks = await _bookService.GetBestsellerBooksAsync();

            return View(_ViewModel);
        }
    }
}
