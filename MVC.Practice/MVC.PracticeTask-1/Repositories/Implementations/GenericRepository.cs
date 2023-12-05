using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.Models;
using System.Linq.Expressions;
using System.Linq;
using MVC.PracticeTask_1.DataAccessLayer;

namespace MVC.PracticeTask_1.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly AppDbContext _DbContext;

        public GenericRepository(AppDbContext context)
        {
            _DbContext = context;
        }

        public DbSet<TEntity> Table => _DbContext.Set<TEntity>();

        public async Task<int> CommitAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes) //{"Authors","BookImages", "BookTags.Tag"}
        {
            var query = GetQuery(includes);

            return expression is not null
                        ? await query.Where(expression).ToListAsync()
                        : await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>>? expression = null, params string[]? includes)
        {
            var query = GetQuery(includes);

            return expression is not null
                    ? await query.Where(expression).FirstOrDefaultAsync()
                    : await query.FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
    }
}
