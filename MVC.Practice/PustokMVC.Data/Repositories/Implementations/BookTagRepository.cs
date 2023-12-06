using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;

namespace MVC.Practice.PustokMVC.Data.Repositories.Implementations
{
    public class BookTagRepository : GenericRepository<BookTag>, IBookTagsRepository
    {
        public BookTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
