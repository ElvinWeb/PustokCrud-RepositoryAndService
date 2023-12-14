using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
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

        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _context.Orders.Include(x => x.OrderItems).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Order order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
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

            return RedirectToAction("Index","Order");
        }

        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            Order order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order is null) return NotFound();

            order.OrderStatus = OrderStatus.Rejected;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Order");
        }     
    }
}
