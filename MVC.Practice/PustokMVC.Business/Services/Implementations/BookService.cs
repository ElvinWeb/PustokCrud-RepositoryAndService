using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Core.Repositories;
using MVC.Practice.PustokMVC.Business.Exceptions.BookExceptions;
using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using MVC.Practice.PustokMVC.Business.Helpers;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.Practice.PustokMVC.Business.Services;

namespace MVC.PracticeTask_1.Services.Implementations
{

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBookTagsRepository _bookTagsRepository;
        private readonly IBookImagesRepository _bookImagesRepository;
        private readonly IWebHostEnvironment _env;

        public BookService(IBookRepository bookRepository,
                           IGenreRepository genreRepository,
                           IAuthorRepository authorRepository,
                           ITagRepository tagRepository,
                           IBookTagsRepository bookTagsRepository,
                           IWebHostEnvironment env,
                           IBookImagesRepository bookImagesRepository)

        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
            _bookTagsRepository = bookTagsRepository;
            _bookImagesRepository = bookImagesRepository;
            _env = env;
        }

        public async Task CreateAsync(Book entity)
        {
            if (!_genreRepository.Table.Any(x => x.Id == entity.GenreId))
            {
                throw new NotFound("GenreId", "Genre not found!");
            }

            if (!_authorRepository.Table.Any(x => x.Id == entity.AuthorId))
            {
                throw new NotFound("AuthorId", "Author not found!");
            }


            bool check = false;

            if (entity.TagIds != null)
            {
                foreach (var tagId in entity.TagIds)
                {
                    if (!_tagRepository.Table.Any(x => x.Id == tagId))
                    {
                        check = true;
                        break;
                    }
                }
            }

            if (check)
            {
                throw new NotFound("TagId", "Tag not found!");
            }
            else
            {
                if (entity.TagIds != null)
                {
                    foreach (var tagId in entity.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = entity,
                            TagId = tagId
                        };

                        await _bookTagsRepository.CreateAsync(bookTag);
                    }
                }
            }

            if (entity.BookMainImage != null)
            {
                if (entity.BookMainImage.ContentType != "image/jpeg" && entity.BookMainImage.ContentType != "image/png")
                {
                    throw new InvalidContentTypeOrImageSize("BookMainImage", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookMainImage.Length > 2097152)
                {
                    throw new InvalidContentTypeOrImageSize("BookMainImage", "File size must be lower than 2mb!");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/Books", entity.BookMainImage),
                    isPoster = true
                };

                await _bookImagesRepository.CreateAsync(bookImage);
            }

            if (entity.BookHoverImage != null)
            {
                if (entity.BookHoverImage.ContentType != "image/jpeg" && entity.BookHoverImage.ContentType != "image/png")
                {
                    throw new InvalidContentTypeOrImageSize("BookHoverImage", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookHoverImage.Length > 2097152)
                {
                    throw new InvalidContentTypeOrImageSize("BookHoverImage", "File size must be lower than 2mb)");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/books", entity.BookHoverImage),
                    isPoster = false
                };

                await _bookImagesRepository.CreateAsync(bookImage);
            }


            if (entity.ImageFiles != null)
            {
                foreach (var imageFile in entity.ImageFiles)
                {
                    if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                    {
                        throw new InvalidContentTypeOrImageSize("ImageFiles", "File must be .png or .jpeg (.jpg)");
                    }
                    if (imageFile.Length > 2097152)
                    {
                        throw new InvalidContentTypeOrImageSize("ImageFiles", "File size must be lower than 2mb)");
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book = entity,
                        ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/books", imageFile),
                        isPoster = null
                    };

                    await _bookImagesRepository.CreateAsync(bookImage);
                }
            }

            await _bookRepository.CreateAsync(entity);
            await _bookRepository.CommitAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync(x => x.IsDeleted == false, "BookImages", "Author", "Genre");
        }


        public async Task<List<Book>> GetBestsellerBooksAsync()
        {
            return await _bookRepository.GetAllAsync(x => x.isBestseller == true, "BookImages", "Author");
        }

        public async Task<List<Book>> GetFeaturedBooksAsync()
        {
            return await _bookRepository.GetAllAsync(x => x.isFeatured == true, "BookImages", "Author");
        }

        public async Task<List<Book>> GetNewBooksAsync()
        {
            return await _bookRepository.GetAllAsync(x => x.isNew == true, "BookImages", "Author");

        }
        public async Task<List<Book>> GetAllRelatedBooksAsync(Book book)
        {
            return await _bookRepository.GetAllAsync(x => x.IsDeleted == false && x.GenreId == book.GenreId && x.Id != book.Id, "BookImages", "Author", "Genre", "BookTags.Tag");
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            Book entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false, "Author", "BookImages", "BookTags.Tag");

            if (entity is null) throw new NullReferenceException();

            return entity;
        }


        public async Task SoftDelete(int id)
        {
            Book entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            entity.IsDeleted = true;

            await _bookRepository.CommitAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            Book existBook = await _bookRepository.GetByIdAsync(x => x.Id == entity.Id && x.IsDeleted == false, "Author", "BookImages", "BookTags.Tag");

            if (existBook == null) throw new NotFound();

            if (!_genreRepository.Table.Any(x => x.Id == entity.GenreId))
            {
                throw new NotFound("GenreId", "Genre not found!");
            }

            if (!_authorRepository.Table.Any(x => x.Id == entity.AuthorId))
            {

                throw new NotFound("AuthorId", "Author not found!");
            }


            existBook.BookTags.RemoveAll(bt => !entity.TagIds.Contains(bt.TagId));

            foreach (var tagId in entity.TagIds.Where(x => !existBook.BookTags.Any(bt => bt.TagId == x)))
            {
                BookTag bookTag = new BookTag
                {
                    Book = existBook,
                    TagId = tagId
                };
                existBook.BookTags.Add(bookTag);
            }


            if (entity.BookMainImage != null)
            {
                if (entity.BookMainImage.ContentType != "image/jpeg" && entity.BookMainImage.ContentType != "image/png")
                {

                    throw new InvalidContentTypeOrImageSize("BookMainImage", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookMainImage.Length > 2097152)
                {
                    throw new InvalidContentTypeOrImageSize("BookMainImage", "File must be .png or .jpeg (.jpg)");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/Books", entity.BookMainImage),
                    isPoster = true
                };

                existBook.BookImages.Add(bookImage);
            }

            if (entity.BookHoverImage != null)
            {
                if (entity.BookHoverImage.ContentType != "image/jpeg" && entity.BookHoverImage.ContentType != "image/png")
                {
                    throw new InvalidContentTypeOrImageSize("BookHoverImage", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookHoverImage.Length > 2097152)
                {
                    throw new InvalidContentTypeOrImageSize("BookHoverImage", "File must be .png or .jpeg (.jpg)");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/books", entity.BookHoverImage),
                    isPoster = false
                };

                existBook.BookImages.Add(bookImage);
            }


            existBook.BookImages.RemoveAll(bi => !entity.BookImageIds.Contains(bi.Id) && bi.isPoster == null);

            if (entity.ImageFiles != null)
            {
                foreach (var imageFile in entity.ImageFiles)
                {
                    if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                    {
                        throw new InvalidContentTypeOrImageSize("ImageFiles", "File must be .png or .jpeg (.jpg)");
                    }
                    if (imageFile.Length > 2097152)
                    {
                        throw new InvalidContentTypeOrImageSize("ImageFiles", "File must be .png or .jpeg (.jpg)");
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book = entity,
                        ImgUrl = Helper.GetFileName(_env.WebRootPath, "uploads/books", imageFile),
                        isPoster = null
                    };


                    existBook.BookImages.Add(bookImage);
                }
            }



            existBook.Name = entity.Name;
            existBook.Desc = entity.Desc;
            existBook.CostPrice = entity.CostPrice;
            existBook.SalePrice = entity.SalePrice;
            existBook.Code = entity.Code;
            existBook.DiscountPercent = entity.DiscountPercent;
            existBook.IsAvailable = entity.IsAvailable;
            existBook.Tax = entity.Tax;
            existBook.AuthorId = entity.AuthorId;
            existBook.GenreId = entity.GenreId;

            await _bookRepository.CommitAsync();

        }

        public IQueryable<Book> GetBookTable()
        {
            var query = _bookRepository.Table.AsQueryable();

            return query;
        }
    }
}
