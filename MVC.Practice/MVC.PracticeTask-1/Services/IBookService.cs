using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface IBookService
    {
        Task CreateAsync(Book entity);
        Task SoftDelete(int id);
        Task Delete(int id);
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task UpdateAsync(Book entity);
    }
}
