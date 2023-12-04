using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Exceptions.CommonModelsExceptions;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.Services;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> Index()
        {
            List<Author> Authors = await _authorService.GetAllAsync();

            return View(Authors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _authorService.CreateAsync(author);
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

            Author author = await _authorService.GetByIdAsync(id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Author author)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _authorService.UpdateAsync(author);
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

            await _authorService.Delete(id);

            return Ok();
        }


    }
}
