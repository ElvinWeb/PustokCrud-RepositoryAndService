using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Services
{
    public interface IBookService
    {
        Task CreateAsync(Book book);
        Task DeleteAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task<List<Author>> GetAllAuthorAsync();
        Task<List<Genre>> GetAllGenreAsync();
        Task<List<Tag>> GetAllTagAsync();
        Task<Book> GetAsync(int id);
        Task UpdateAsync(Book book);
    }
}
