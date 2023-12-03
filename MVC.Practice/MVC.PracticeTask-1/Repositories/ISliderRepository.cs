using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories
{
    public interface ISliderRepository
    {
        Task CreateAsync(Slide slide);
        void Delete(Slide slide);
        Task<Slide> GetSlideByIdAsync(int id);
        Task<List<Slide>> GetAllAsync();
        Task<int> SaveAsync();
    }
}
