using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Exceptions.CommonModelsExceptions;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.Services;
using MVC.PracticeTask_1.Services.Implementations;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {

        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            List<Genre> Genres = await _genreService.GetAllAsync();

            return View(Genres);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                await _genreService.CreateAsync(genre);
            }
            catch (InvalidAlreadyCreated ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return NotFound();

            Genre genre = await _genreService.GetByIdAsync(id);

            if (genre == null) return NotFound();

            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Genre genre)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _genreService.UpdateAsync(genre);
            }
            catch (InvalidAlreadyCreated ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            await _genreService.Delete(id);

            return Ok();
        }


    }
}
