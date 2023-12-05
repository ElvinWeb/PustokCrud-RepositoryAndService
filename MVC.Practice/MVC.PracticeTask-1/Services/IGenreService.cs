using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface IGenreService
    {
        Task CreateAsync(Genre entity);
        Task Delete(int id);
   
        Task<Genre> GetByIdAsync(int id);
        Task<List<Genre>> GetAllAsync();
        Task UpdateAsync(Genre entity);
    }
}
