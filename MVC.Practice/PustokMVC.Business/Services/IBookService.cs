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
<<<<<<< HEAD
        Task<List<Book>> GetNewBooksAsync();
        Task<List<Book>> GetBestsellerBooksAsync();
        Task<List<Book>> GetFeaturedBooksAsync();
=======
        Task<List<Book>> GetAllNewBooksAsync();
        Task<List<Book>> GetAllBestsellerAsync();
        Task<List<Book>> GetAllFeaturedAsync();
>>>>>>> 070b16653b75c086ecfc1746423ec366c1523347
        Task UpdateAsync(Book entity);
    }
}
