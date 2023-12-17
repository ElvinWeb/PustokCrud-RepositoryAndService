using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.PracticeTask_1.Pagination;
using MVC.PracticeTask_1.ViewModel;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _tagService.GetTagTable();

            PaginatedList<Tag> paginatedTags = PaginatedList<Tag>.Create(query, page, 3);

            return View(paginatedTags);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _tagService.CreateAsync(tag);
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

            Tag tag = await _tagService.GetByIdAsync(id);

            if (tag == null) return NotFound();

            return View(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tag tag)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _tagService.CreateAsync(tag);
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

            await _tagService.Delete(id);

            return Ok();
        }


    }
}
