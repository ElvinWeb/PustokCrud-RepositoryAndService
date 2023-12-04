using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class BookTagRepository : GenericRepository<BookTag>, IBookTagsRepository
    {
        public BookTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}
