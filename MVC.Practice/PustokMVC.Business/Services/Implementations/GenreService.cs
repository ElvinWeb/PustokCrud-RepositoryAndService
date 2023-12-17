using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Data.Repositories.Implementations;

namespace MVC.PracticeTask_1.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task CreateAsync(Genre entity)
        {
            if (_genreRepository.Table.Any(x => x.Name.ToLower() == entity.Name.ToLower()))
            {
                throw new InvalidAlreadyCreated("Name", "genre has already created!");
            }

            await _genreRepository.CreateAsync(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Genre entity = await _genreRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            _genreRepository.Delete(entity);
            await _genreRepository.CommitAsync();
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _genreRepository.GetAllAsync(x => x.IsDeleted == false, "Books");
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity is null) throw new NullReferenceException();
            return entity;
        }

        public IQueryable<Genre> GetGenreTable()
        {
            var query = _genreRepository.Table.AsQueryable();

            return query;
        }

        public async Task UpdateAsync(Genre genre)
        {
            Genre existEntity = await _genreRepository.GetByIdAsync(x => x.Id == genre.Id && x.IsDeleted == false);

            if (_genreRepository.Table.Any(x => x.Name.ToLower() == genre.Name.ToLower() && existEntity.Id != genre.Id))
            {
                throw new InvalidAlreadyCreated("Name", "genre has already created!");
            }

            existEntity.Name = genre.Name;

            await _genreRepository.CommitAsync();
        }
    }
}
