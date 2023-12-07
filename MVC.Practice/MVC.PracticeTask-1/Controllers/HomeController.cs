using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Controllers
{
    public class HomeController : Controller
    {
<<<<<<< HEAD
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
=======
        private readonly AppDbContext _DbContext;
        private readonly ISliderService _slideService;
        private readonly IBookService _bookService;
        private AppViewModel _ViewModel = new AppViewModel();
        public HomeController(AppDbContext _context, ISliderService sliderService, IBookService bookService)
        {
            _DbContext = _context;
            _slideService = sliderService;
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            _ViewModel.Slides = await _slideService.GetAllAsync();
            _ViewModel.Services = _DbContext.Services.ToList();
            _ViewModel.NewBooks = await _bookService.GetAllNewBooksAsync();
            _ViewModel.FeaturedBooks = await _bookService.GetAllFeaturedAsync();
            _ViewModel.BestsellerBooks = await _bookService.GetAllBestsellerAsync();
>>>>>>> 070b16653b75c086ecfc1746423ec366c1523347


            return View(_ViewModel);
        }
    }
}
