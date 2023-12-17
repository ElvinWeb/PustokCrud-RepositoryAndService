using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Data.Repositories.Implementations;

namespace MVC.PracticeTask_1.Services.Implementations
{
    public class AuhtorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuhtorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task CreateAsync(Author entity)
        {

            if (_authorRepository.Table.Any(a => a.FullName.ToLower() == entity.FullName.ToLower()))
            {
                throw new InvalidAlreadyCreated("FullName", "Author has already created!");
            }

            await _authorRepository.CreateAsync(entity);

            await _authorRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Author entity = await _authorRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity == null) throw new NullReferenceException();

            _authorRepository.Delete(entity);
            await _authorRepository.CommitAsync();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var entity = await _authorRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            return entity;
        }

        public IQueryable<Author> GetAuthorTable()
        {
            var query = _authorRepository.Table.AsQueryable();

            return query;
        }

        public async Task UpdateAsync(Author author)
        {
            Author existEntity = await _authorRepository.GetByIdAsync(x => x.Id == author.Id && x.IsDeleted == false);

            if (existEntity == null) throw new NotFound();

            if (_authorRepository.Table.Any(x => x.FullName.ToLower() == author.FullName.ToLower() && existEntity.Id != author.Id))
            {
                throw new InvalidAlreadyCreated("FullName", "Author has already created!");
            }

            existEntity.FullName = author.FullName;

            await _authorRepository.CommitAsync();
        }
    }
}
