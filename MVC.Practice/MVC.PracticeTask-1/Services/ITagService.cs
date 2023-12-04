using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface ITagService
    {
        Task CreateAsync(Tag entity);
        Task Delete(int id);
        Task<Tag> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task UpdateAsync(Tag entity);
    }
}
