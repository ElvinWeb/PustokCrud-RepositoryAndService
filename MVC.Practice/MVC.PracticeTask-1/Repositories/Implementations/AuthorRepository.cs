using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
