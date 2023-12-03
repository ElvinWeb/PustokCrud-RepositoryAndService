using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface ISliderService
    {
        Task CreateAsync(Slide slide);
        Task DeleteAsync(int id);
        Task<List<Slide>> GetAllAsync();
        Task<Slide> GetAsync(int id);
        Task UpdateAsync(Slide slide);

    }
}
