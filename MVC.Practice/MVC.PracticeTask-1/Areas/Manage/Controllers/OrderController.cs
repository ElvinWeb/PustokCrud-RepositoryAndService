using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.PracticeTask_1.Pagination;
using PustokMVC.Core.Enums;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "Admin,SuperAdmin,Member")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Orders.AsQueryable();

            PaginatedList<Order> paginatedOrders = PaginatedList<Order>.Create(query, page, 3);


            return View(paginatedOrders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Order order = await _context.Orders.Include(x => x.OrderItems).ThenInclude(book => book.Book).ThenInclude(img => img.BookImages).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null) return NotFound();

            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Accept(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order is null) return NotFound();

            order.OrderStatus = OrderStatus.Accepted;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string AdminComment)
        {
            Order order = await _context.Orders.Include(item => item.OrderItems).ThenInclude(book => book.Book).ThenInclude(img => img.BookImages).FirstOrDefaultAsync(x => x.Id == id);

            if (order is null) return NotFound();
            if (AdminComment == null)
            {
                ModelState.AddModelError("AdminComment", "Must be written!");
                return View("detail", order);
            }

            order.OrderStatus = OrderStatus.Rejected;
            order.AdminComment = AdminComment;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Order");
        }
    }
}
