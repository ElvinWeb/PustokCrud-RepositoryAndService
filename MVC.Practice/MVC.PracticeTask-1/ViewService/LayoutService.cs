using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using PustokMVC.Core.Models;

namespace MVC.PracticeTask_1.ViewService
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context, IBookService bookService)
        {
            _context = context;

        }

        public async Task<List<Setting>> GetSetting()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            return settings;
        }

    }
}
