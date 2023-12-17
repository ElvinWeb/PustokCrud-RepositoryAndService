using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.Practice.PustokMVC.Business.Services
{
    public interface ITagService
    {
        Task CreateAsync(Tag entity);
        Task Delete(int id);
        IQueryable<Tag> GetTagTable();
        Task<Tag> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task UpdateAsync(Tag entity);
    }
}
