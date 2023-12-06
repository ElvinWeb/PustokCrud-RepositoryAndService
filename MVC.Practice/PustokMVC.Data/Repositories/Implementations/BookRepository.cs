using MVC.Practice.PustokMVC.Data.DataAccessLayer;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Core.Repositories;

namespace MVC.Practice.PustokMVC.Data.Repositories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        #region oldCodes

        //public async Task CreateAsync(Book book)
        //{
        //    await _DbContext.Books.AddAsync(book);
        //}

        //public async Task CreateBookImageAsync(BookImage bookimage)
        //{
        //    await _DbContext.BookImages.AddAsync(bookimage);
        //}

        //public async Task CreateBookTagAsync(BookTag booktag)
        //{
        //    await _DbContext.BookTags.AddAsync(booktag);
        //}

        //public void Delete(Book book)
        //{
        //    _DbContext.Books.Remove(book);
        //}

        //public async Task<List<Book>> GetAllAsync()
        //{
        //    return await _DbContext.Books.ToListAsync();
        //}

        //public async Task<List<Author>> GetAllAuthorAsync()
        //{
        //    return await _DbContext.Authors.ToListAsync();
        //}

        //public async Task<List<BookImage>> GetAllBookImagesAsync()
        //{
        //    return await _DbContext.BookImages.ToListAsync();
        //}

        //public async Task<List<BookTag>> GetAllBookTagAsync()
        //{
        //    return await _DbContext.BookTags.ToListAsync();
        //}

        //public async Task<List<Genre>> GetAllGenreAsync()
        //{
        //    return await _DbContext.Genres.ToListAsync();
        //}

        //public async Task<List<Tag>> GetAllTagAsync()
        //{
        //    return await _DbContext.Tags.ToListAsync();
        //}

        //public async Task<Book> GetBookByIdAsync(int id)
        //{
        //    return await _DbContext.Books.Include(x => x.BookImages).Include(y => y.BookTags).FirstOrDefaultAsync(b => b.Id == id);
        //}

        //public async Task<int> SaveAsync()
        //{
        //    return await _DbContext.SaveChangesAsync();
        //}

        #endregion
        public BookRepository(AppDbContext context) : base(context)
        {

        }
    }
}
