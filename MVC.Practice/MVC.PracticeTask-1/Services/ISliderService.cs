using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface ISliderService
    {
        Task CreateAsync(Slide entity);
        Task Delete(int id);
        Task<Slide> GetByIdAsync(int id);
        Task<List<Slide>> GetAllAsync();
        Task UpdateAsync(Slide entity);

    }
}
