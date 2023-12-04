using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface IAuthorService
    {
        Task CreateAsync(Author entity);
        Task Delete(int id);
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task UpdateAsync(Author entity);
    }
}
