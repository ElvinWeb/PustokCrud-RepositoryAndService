using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;    
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;

        public ShopController(AppDbContext context, IGenreService genreService, IAuthorService authorService)
        {
            _context = context;
            _genreService = genreService;
            _authorService = authorService;
        }
        public async Task<IActionResult> Index(int? genreId, int? authorId)
        {
            var query = _context.Books.Include(x => x.BookImages).AsQueryable();

            if (genreId != null)
            {
                query = query.Where(x => x.GenreId == genreId);
            }

            if (authorId != null)
            {
                query = query.Where(x => x.AuthorId == authorId);
            }

            ShopViewModel shopVM = new ShopViewModel()
            {
                Books = await query.ToListAsync(),
                Genres = await _genreService.GetAllAsync(),
                Authors = await _authorService.GetAllAsync(),   

            };

            return View(shopVM);
        }
    }
}
