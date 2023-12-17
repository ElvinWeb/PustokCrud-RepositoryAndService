using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.Practice.PustokMVC.Business.Services
{
    public interface IGenreService
    {
        Task CreateAsync(Genre entity);
        Task Delete(int id);
        IQueryable<Genre> GetGenreTable();
        Task<Genre> GetByIdAsync(int id);
        Task<List<Genre>> GetAllAsync();
        Task UpdateAsync(Genre entity);
    }
}
