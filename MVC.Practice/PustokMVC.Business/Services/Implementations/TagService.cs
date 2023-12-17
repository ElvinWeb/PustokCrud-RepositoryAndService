using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Business.Services;

namespace MVC.PracticeTask_1.Services.Implementations
{
    public class TagService : ITagService
    {

        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task CreateAsync(Tag entity)
        {
            if (_tagRepository.Table.Any(x => x.Name.ToLower() == entity.Name.ToLower()))
            {
                throw new InvalidAlreadyCreated("Name", "tag has already created!");
            }

            await _tagRepository.CreateAsync(entity);
            await _tagRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            Tag entity = await _tagRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            _tagRepository.Delete(entity);
            await _tagRepository.CommitAsync();
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            Tag entity = await _tagRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (entity is null) throw new NullReferenceException();
            return entity;
        }

        public IQueryable<Tag> GetTagTable()
        {
            var query = _tagRepository.Table.AsQueryable();

            return query;
        }

        public async Task UpdateAsync(Tag tag)
        {
            Tag existEntity = await _tagRepository.GetByIdAsync(x => x.Id == tag.Id && x.IsDeleted == false);

            if (_tagRepository.Table.Any(x => x.Name.ToLower() == tag.Name.ToLower() && existEntity.Id != tag.Id))
            {
                throw new InvalidAlreadyCreated("Name", "tag has already created!");
            }

            existEntity.Name = tag.Name;

            await _tagRepository.CommitAsync();
        }
    }
}
