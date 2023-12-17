using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Exceptions.SliderExceptions;
using MVC.Practice.PustokMVC.Business.Helpers;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Data.Repositories.Implementations;

namespace MVC.PracticeTask_1.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISliderRepository _sliderRepository;
        public SliderService(IWebHostEnvironment env, ISliderRepository sliderRepository)
        {
            _env = env;
            _sliderRepository = sliderRepository;

        }
        public async Task CreateAsync(Slide slide)
        {
            if (slide.Image != null)
            {

                if (slide.Image.ContentType != "image/png" && slide.Image.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("Image", "please select correct file type");
                }

                if (slide.Image.Length > 1048576)
                {
                    throw new InvalidImageSize("Image", "file size should be more lower than 1mb");
                }
            }
            else
            {
                throw new InvalidImage("Image", "image file is must be choosed!! ");
            }

            string folder = "uploads/bg-slide";
            string newFileName = Helper.GetFileName(_env.WebRootPath, folder, slide.Image);

            slide.ImgUrl = newFileName;

            await _sliderRepository.CreateAsync(slide);
            await _sliderRepository.CommitAsync();

        }

        public async Task DeleteAsync(int id)
        {
            if (id == null) throw new InvalidNullReferance();

            Slide wantedSlide = await _sliderRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (wantedSlide == null) throw new InvalidNullReferance();

            string fullPath = Path.Combine(_env.WebRootPath, "uploads/bg-slide", wantedSlide.ImgUrl);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            _sliderRepository.Delete(wantedSlide);
            await _sliderRepository.CommitAsync();
        }

        public async Task<List<Slide>> GetAllAsync()
        {
            return await _sliderRepository.GetAllAsync(x => x.IsDeleted == false);
        }

        public async Task<Slide> GetByIdAsync(int id)
        {
            Slide entity = await _sliderRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            return entity;
        }
        public async Task UpdateAsync(Slide slide)
        {
            Slide wantedSlide = await _sliderRepository.GetByIdAsync(x => x.Id == slide.Id && x.IsDeleted == false);

            if (wantedSlide == null) throw new InvalidNullReferance();

            if (slide.Image != null)
            {

                if (slide.Image.ContentType != "image/png" && slide.Image.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("Image", "please select correct file type");
                }

                if (slide.Image.Length > 1048576)
                {
                    throw new InvalidImageSize("Image", "file size should be more lower than 1mb");
                }

                string folderPath = "uploads/bg-slide";

                string expiredFileName = Helper.GetFileName(_env.WebRootPath, folderPath, slide.Image);

                string wantedPath = Path.Combine(_env.WebRootPath, folderPath, wantedSlide.ImgUrl);

                if (File.Exists(wantedPath))
                {
                    File.Delete(wantedPath);
                }

                wantedSlide.ImgUrl = expiredFileName;
            }

            wantedSlide.Title = slide.Title;
            wantedSlide.Description = slide.Description;
            wantedSlide.BtnText = slide.BtnText;
            wantedSlide.RedirectUrl = slide.RedirectUrl;


            await _sliderRepository.CommitAsync();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Slide> GetSlideTable()
        {
            var query = _sliderRepository.Table.AsQueryable();

            return query;
        }
    }
}
