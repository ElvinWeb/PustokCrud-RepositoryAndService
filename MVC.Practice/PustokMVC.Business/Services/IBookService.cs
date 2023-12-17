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
        IQueryable<Book> GetBookTable();
        Task<List<Book>> GetNewBooksAsync();
        Task<List<Book>> GetAllRelatedBooksAsync(Book book);
        Task<List<Book>> GetBestsellerBooksAsync();
        Task<List<Book>> GetFeaturedBooksAsync();
        Task UpdateAsync(Book entity);
    }
}
