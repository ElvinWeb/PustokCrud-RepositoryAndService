using Microsoft.AspNetCore.Mvc;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.PracticeTask_1.ViewModel;
using Newtonsoft.Json;

namespace MVC.PracticeTask_1.Controllers
{
    public class ProductCookiesController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;

        public ProductCookiesController(IBookRepository bookRepository, IBookService bookService)
        {
            _bookRepository = bookRepository;
            _bookService = bookService; 
        }
        public IActionResult Index()
        {
            return View();
        }
        #region SessionAndCookies

        //public IActionResult SetSession(string name)
        //{
        //    HttpContext.Session.SetString("UserName", name);

        //    return Content("Added to session");
        //}

        //public IActionResult GetSession()
        //{
        //    string username = HttpContext.Session.GetString("UserName");

        //    return Content(username);
        //}

        //public IActionResult RemoveSession()
        //{
        //    HttpContext.Session.Remove("UserName");

        //    return RedirectToAction("GetSession");
        //}

        //public IActionResult SetCookie(int id)
        //{
        //    List<int> ids = new List<int>();

        //    string idsStr = HttpContext.Request.Cookies["UserId"];

        //    if (idsStr is not null)
        //    {
        //        ids = JsonConvert.DeserializeObject<List<int>>(idsStr);
        //    }

        //    ids.Add(id);

        //    idsStr = JsonConvert.SerializeObject(ids);

        //    HttpContext.Response.Cookies.Append("UserId", idsStr);

        //    return Content("Added to cookie");
        //}

        //public IActionResult GetCookie()
        //{
        //    List<int> ids = new List<int>();

        //    string idsStr = HttpContext.Request.Cookies["UserId"];
        //    if (idsStr is not null)
        //        ids = JsonConvert.DeserializeObject<List<int>>(idsStr);


        //    return Json(ids);
        //}
        #endregion
        public async Task<IActionResult> Detail(int id)
        {
            Book book = await _bookService.GetByIdAsync(id);

            ProductDetailViewModel productDetailViewModel = new ProductDetailViewModel()
            {
                Book = book,
                RelatedBooks = await _bookService.GetAllRelatedBooksAsync(book)
            };

            return View(productDetailViewModel);
        }

        public async Task<IActionResult> GetBookModal(int id)
        {
            Book book = await _bookService.GetByIdAsync(id);

            return PartialView("_BookModlaPartialView", book);
        }

        public IActionResult AddToBasket(int bookId)
        {
            if (!_bookRepository.Table.Any(x => x.Id == bookId)) return NotFound();

            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            BasketItemViewModel basketItem = null;

            string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];

            if (basketItemListStr is not null)
            {
                basketItemList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemListStr);

                basketItem = basketItemList.FirstOrDefault(b => b.BookId == bookId);

                if (basketItem is not null)
                {
                    basketItem.Count++;
                }
                else
                {
                    basketItem = new BasketItemViewModel()
                    {
                        BookId = bookId,
                        Count = 1,
                    };
                    basketItemList.Add(basketItem);
                }

            }
            else
            {
                basketItem = new BasketItemViewModel()
                {
                    BookId = bookId,
                    Count = 1,
                };
                basketItemList.Add(basketItem);
            }

            basketItemListStr = JsonConvert.SerializeObject(basketItemList);

            HttpContext.Response.Cookies.Append("BasketItems", basketItemListStr);

            return Ok(); //200
        }

        public IActionResult GetBookItems()
        {


            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();

            string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];

            if (basketItemListStr is not null)
            {
                basketItemList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemListStr);
            }
            return Json(basketItemList);
        }

        public async Task<IActionResult> Checkout()
        {
            List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            CheckoutViewModel checkoutItem = null;

            string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];

            if (basketItemListStr is not null)
            {
                basketItemList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemListStr);

                foreach (var item in basketItemList)
                {
                    checkoutItem = new CheckoutViewModel()
                    {
                        Book = await _bookRepository.GetByIdAsync(b => b.Id == item.BookId),
                        Count = item.Count
                    };
                    checkoutItemList.Add(checkoutItem);
                }

            }
            return View(checkoutItemList);

        }
    }
}
