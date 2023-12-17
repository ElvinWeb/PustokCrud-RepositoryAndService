using MVC.Practice.PustokMVC.Core.Models;

namespace MVC.Practice.PustokMVC.Business.Services
{
    public interface ISliderService
    {
        Task CreateAsync(Slide entity);
        Task Delete(int id);
        IQueryable<Slide> GetSlideTable();
        Task<Slide> GetByIdAsync(int id);
        Task<List<Slide>> GetAllAsync();
        Task UpdateAsync(Slide entity);

    }
}
