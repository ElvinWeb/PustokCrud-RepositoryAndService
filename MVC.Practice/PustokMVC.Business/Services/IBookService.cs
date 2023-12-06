using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.Practice.PustokMVC.Business.Services
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
