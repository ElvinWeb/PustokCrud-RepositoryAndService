using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.PracticeTask_1.Pagination;
using MVC.PracticeTask_1.Services;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {

        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {

            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _sliderService.GetSlideTable();

            PaginatedList<Slide> paginatedSlides = PaginatedList<Slide>.Create(query, page, 3);

            return View(paginatedSlides);
        }

        [HttpGet]
        public IActionResult Create()
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

            Slide slide = await _sliderService.GetByIdAsync(id);

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
                await _sliderService.Delete(id);
            }
            catch (InvalidNullReferance)
            {

            }

            return Ok();
        }
    }
}
