using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.DataAccessLayer;
using MVC.PracticeTask_1.Models;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class SliderRepository : GenericRepository<Slide>, ISliderRepository
    {

        #region oldCodes

        //public async Task CreateAsync(Slide slide)
        //{
        //    await _DbContext.Slides.AddAsync(slide);
        //}

        //public void Delete(Slide slide)
        //{
        //    _DbContext.Slides.Remove(slide);
        //}

        //public async Task<List<Slide>> GetAllAsync()
        //{
        //    return await _DbContext.Slides.ToListAsync();
        //}

        //public async Task<Slide> GetSlideByIdAsync(int id)
        //{
        //    return await _DbContext.Slides.FirstOrDefaultAsync(s => s.Id == id);
        //}

        //public async Task<int> SaveAsync()
        //{
        //    return await _DbContext.SaveChangesAsync();
        //}
        #endregion
        public SliderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
