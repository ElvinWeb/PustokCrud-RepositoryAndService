using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class TagRepository : GenericRepository<Slide>, ISliderRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {

        }
    }
}
