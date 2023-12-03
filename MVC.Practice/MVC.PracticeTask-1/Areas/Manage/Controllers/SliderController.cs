using Microsoft.AspNetCore.Mvc;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Exceptions.SliderExceptions;
using MVC.PracticeTask_1.Helpers;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.Services;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _DbContext;
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;
        public SliderController(AppDbContext _context, IWebHostEnvironment env, ISliderService sliderService)
        {
            _DbContext = _context;
            _env = env;
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            List<Slide> allSlides = await _sliderService.GetAllAsync();

            return View(allSlides);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid) return View(slide);

            try
            {
                await _sliderService.CreateAsync(slide);
            }
            catch (InvalidContentType ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImageSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null) return View();

            Slide slide = await _sliderService.GetAsync(id);

            if (slide == null) return NotFound();

            return View(slide);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Slide slide)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _sliderService.UpdateAsync(slide);
            }
            catch (InvalidContentType ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImageSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidImage ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (InvalidNullReferance)
            {

            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            try
            {
                await _sliderService.DeleteAsync(id);
            }
            catch (InvalidNullReferance)
            {

            }

            return Ok();
        }
    }
}
