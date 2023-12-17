using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.Practice.PustokMVC.Business.Services
{
    public interface IAuthorService
    {
        Task CreateAsync(Author entity);
        Task Delete(int id);
        IQueryable<Author> GetAuthorTable();
        Task<Author> GetByIdAsync(int id);
        Task<List<Author>> GetAllAsync();
        Task UpdateAsync(Author entity);
    }
}
