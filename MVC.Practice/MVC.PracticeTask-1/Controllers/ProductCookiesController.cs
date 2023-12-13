using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.ViewModel;
using Newtonsoft.Json;
using PustokMVC.Core.Models;
using System.Collections.Concurrent;

namespace MVC.PracticeTask_1.Controllers
{
    public class ProductCookiesController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _bookService;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public ProductCookiesController(IBookRepository bookRepository, IBookService bookService, UserManager<User> userManager, AppDbContext context)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
            _userManager = userManager;
            _context = context;
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

        public async Task<IActionResult> AddToBasket(int bookId)
        {
            if (!_bookRepository.Table.Any(x => x.Id == bookId)) return NotFound();

            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            BasketItemViewModel basketItem = null;
            BasketItem userBasketItem = null;
            User user = null;


            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user is null)
            {
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
            }
            else
            {
                userBasketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.BookId == bookId && x.UserId == user.Id);
                if (userBasketItem != null)
                {
                    userBasketItem.Count++;
                }
                else
                {
                    userBasketItem = new BasketItem
                    {
                        BookId = bookId,
                        Count = 1,
                        UserId = user.Id,
                        IsDeleted = false
                    };
                    _context.BasketItems.Add(userBasketItem);
                }
                await _context.SaveChangesAsync();
            }



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

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            List<BasketItem> userBasketItems = new List<BasketItem>();
            CheckoutViewModel checkoutItem = null;
            User user = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            if (user is null)
            {

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
            }
            else
            {
                userBasketItems = await _context.BasketItems.Include(x => x.Book).Where(x => x.UserId == user.Id).ToListAsync();

                foreach (var item in userBasketItems)
                {
                    checkoutItem = new CheckoutViewModel
                    {
                        Book = item.Book,
                        Count = item.Count
                    };
                    checkoutItemList.Add(checkoutItem);
                }
            }

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                CheckoutViewModels = checkoutItemList,
                FullName = user.FullName,
                Email = user.Email,
                
            };
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid) return View();

            List<CheckoutViewModel> checkoutItemList = new List<CheckoutViewModel>();
            List<BasketItemViewModel> basketItemList = new List<BasketItemViewModel>();
            List<BasketItem> userBasketItems = new List<BasketItem>();
            CheckoutViewModel checkoutItem = null;
            User user = null;
            OrderItem orderItem = null;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }

            Order order = new Order()
            {
                FullName = orderViewModel.FullName,
                Country = orderViewModel.Country,
                Address = orderViewModel.Address,
                Email = orderViewModel.Email,
                ZipCode = orderViewModel.ZipCode,
                Phone = orderViewModel.Phone,
                Note = orderViewModel.Note,
                UserId = user?.Id,
                OrderItems = new List<OrderItem>(),

            };


            if (user is null)
            {

                string basketItemListStr = HttpContext.Request.Cookies["BasketItems"];

                if (basketItemListStr is not null)
                {
                    basketItemList = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemListStr);

                    foreach (var item in basketItemList)
                    {
                        Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == item.BookId);

                        orderItem = new OrderItem()
                        {
                            Book = book,
                            BookName = book.Name,
                            CostPrice = book.CostPrice,
                            DiscountPercent = book.DiscountPercent,
                            SalePrice = book.SalePrice * ((100 - book.DiscountPercent) / 100),
                            Count = item.Count,
                            Order = order,
                        };

                        order.TotalPrice += orderItem.SalePrice * orderItem.Count;
                        order.OrderItems.Add(orderItem);
                    }

                }
            }
            else
            {
                userBasketItems = await _context.BasketItems.Include(x => x.Book).Where(x => x.UserId == user.Id).ToListAsync();

                foreach (var item in userBasketItems)
                {
                    Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == item.BookId);

                    orderItem = new OrderItem()
                    {
                        Book = book,
                        BookName = book.Name,
                        CostPrice = book.CostPrice,
                        DiscountPercent = book.DiscountPercent,
                        SalePrice = book.SalePrice * ((100 - book.DiscountPercent) / 100),
                        Count = item.Count,
                        Order = order,
                    };

                    order.TotalPrice += orderItem.SalePrice * orderItem.Count;
                    order.OrderItems.Add(orderItem);
                }
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }

}

