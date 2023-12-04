using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class BookImagesRepository : GenericRepository<BookImage>, IBookImagesRepository
    {
        public BookImagesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
