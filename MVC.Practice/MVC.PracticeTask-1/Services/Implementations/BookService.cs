using Microsoft.EntityFrameworkCore;
using MVC.PracticeTask_1.Exceptions.BookExceptions;
using MVC.PracticeTask_1.Helpers;
using MVC.PracticeTask_1.Models;
using MVC.PracticeTask_1.Repositories;
using MVC.PracticeTask_1.Repositories.Implementations;

namespace MVC.PracticeTask_1.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBookRepository _bookRepository;
        public BookService(IWebHostEnvironment env, IBookRepository bookRepository)
        {
            _env = env;
            _bookRepository = bookRepository;

        }
        public async Task CreateAsync(Book book)
        {

            List<Author> Authors = await _bookRepository.GetAllAuthorAsync();
            List<Genre> Genres = await _bookRepository.GetAllGenreAsync();
            List<Tag> Tags = await _bookRepository.GetAllTagAsync();
            List<BookTag> BookTags = await _bookRepository.GetAllBookTagAsync();
            List<BookImage> BookImages = await _bookRepository.GetAllBookImagesAsync();

            if (!Authors.Any(a => a.Id == book.AuthorId))
            {
                throw new InvalidAuthorId("AuthorId", "author is not found!");
            }

            if (!Genres.Any(g => g.Id == book.GenreId))
            {
                throw new InvalidGenreId("GenreId", "genre is not found!");
            }

            bool check = false;

            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!Tags.Any(t => t.Id == tagId))
                    {
                        check = true;
                        break;
                    }

                }
            }

            if (check)
            {
                throw new InvalidTagId("TagId", "tag is not found!");
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag bookTag = new BookTag()
                        {
                            Book = book,
                            TagId = tagId,
                        };

                        await _bookRepository.CreateBookTagAsync(bookTag);
                    }
                }
            }


            if (book.BookMainImage != null)
            {

                if (book.BookMainImage.ContentType != "image/png" && book.BookMainImage.ContentType != "image/jpeg")
                {

                    throw new InvalidContentType("BookMainImage", "please select correct file type");
                }

                if (book.BookMainImage.Length > 1048576)
                {
                    throw new InvalidImageSize("BookMainImage", "file size should be more lower than 1mb ");
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookMainImage);
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImgUrl = newFileName,
                    isPoster = true,
                };

                await _bookRepository.CreateBookImageAsync(bookImage);
            };


            if (book.BookHoverImage != null)
            {

                if (book.BookHoverImage.ContentType != "image/png" && book.BookHoverImage.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("BookHoverImage", "please select correct file type");
                }

                if (book.BookHoverImage.Length > 1048576)
                {
                    throw new InvalidImageSize("BookHoverImage", "file size should be more lower than 1mb ");
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookHoverImage);
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImgUrl = newFileName,
                    isPoster = false,
                };
                await _bookRepository.CreateBookImageAsync(bookImage);
            };

            if (book.ImageFiles != null)
            {
                foreach (var img in book.ImageFiles)
                {
                    string fileName = img.FileName;

                    if (img.ContentType != "image/png" && img.ContentType != "image/jpeg")
                    {
                        throw new InvalidContentType("ImageFiles", "please select correct file type");
                    }

                    if (img.Length > 1048576)
                    {
                        throw new InvalidImageSize("ImageFiles", "file size should be more lower than 1mb ");
                    }

                    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", img);
                    BookImage bookImage = new BookImage
                    {
                        Book = book,
                        ImgUrl = newFileName,
                        isPoster = null,
                    };
                    await _bookRepository.CreateBookImageAsync(bookImage);
                }
            }

            await _bookRepository.CreateAsync(book);
            await _bookRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id == null) throw new InvalidNullReferance();

            Book wantedBook = await _bookRepository.GetBookByIdAsync(id);

            if (wantedBook == null) throw new InvalidNullReferance();

            if (wantedBook.BookImages != null)
            {
                foreach (var item in wantedBook.BookImages)
                {
                    if (item.isPoster == true && item.isPoster == false && item.isPoster == null)
                    {
                        string fullPath = Path.Combine(_env.WebRootPath, "uploads/books", item.ImgUrl);

                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }

                }
            }

            _bookRepository.Delete(wantedBook);
            await _bookRepository.SaveAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<List<Author>> GetAllAuthorAsync()
        {
            return await _bookRepository.GetAllAuthorAsync();
        }

        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await _bookRepository.GetAllGenreAsync();
        }

        public async Task<List<Tag>> GetAllTagAsync()
        {
            return await _bookRepository.GetAllTagAsync();
        }

        public async Task<Book> GetAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task UpdateAsync(Book book)
        {
            List<Book> Books = await _bookRepository.GetAllAsync();
            List<Author> Authors = await _bookRepository.GetAllAuthorAsync();
            List<Genre> Genres = await _bookRepository.GetAllGenreAsync();
            List<Tag> Tags = await _bookRepository.GetAllTagAsync();
            List<BookTag> BookTags = await _bookRepository.GetAllBookTagAsync();
            List<BookImage> BookImages = await _bookRepository.GetAllBookImagesAsync();

         
            Book existBook = await _bookRepository.GetBookByIdAsync(book.Id);

            if (existBook == null) throw new InvalidNullReferance();

            if (!Authors.Any(a => a.Id == book.AuthorId))
            {
                throw new InvalidAuthorId("AuthorId", "author is not found!");
            }

            if (!Genres.Any(g => g.Id == book.GenreId))
            {
                throw new InvalidGenreId("GenreId", "genre is not found!");
            }

            existBook.BookTags.RemoveAll(bt => !book.TagIds.Any(tId => tId == bt.TagId));

            foreach (var id in book.TagIds.Where(bt => !existBook.BookTags.Any(tId => bt == tId.TagId)))
            {
                BookTag bookTag = new BookTag()
                {
                    TagId = id,
                };

                existBook.BookTags.Add(bookTag);

            }


            if (book.BookMainImage != null)
            {
                string folderPath = "uploads/books";
                string path = Path.Combine(_env.WebRootPath, folderPath, existBook.BookImages.FirstOrDefault(x => x.isPoster == true).ImgUrl);

                existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == true);


                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (book.BookMainImage.ContentType != "image/png" && book.BookMainImage.ContentType != "image/jpeg")
                {

                    throw new InvalidContentType("BookMainImage", "please select correct file type");
                }

                if (book.BookMainImage.Length > 1048576)
                {
                    throw new InvalidImageSize("BookMainImage", "file size should be more lower than 1mb");
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookMainImage);
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImgUrl = newFileName,
                    isPoster = true,
                };
                existBook.BookImages.Add(bookImage);
            };

            if (book.BookHoverImage != null)
            {
                string folderPath = "uploads/books";
                string path = Path.Combine(_env.WebRootPath, folderPath, existBook.BookImages.Where(x => x.isPoster == false).FirstOrDefault().ImgUrl);
                existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == false);


                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (book.BookHoverImage.ContentType != "image/png" && book.BookHoverImage.ContentType != "image/jpeg")
                {
                    throw new InvalidContentType("BookHoverImage", "please select correct file type");
                }

                if (book.BookHoverImage.Length > 1048576)
                {
                    throw new InvalidImageSize("BookHoverImage", "file size should be more lower than 1mb");
                }

                string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookHoverImage);
                BookImage bookImage = new BookImage
                {
                    Book = book,
                    ImgUrl = newFileName,
                    isPoster = false,
                };
                existBook.BookImages.Add(bookImage);
            };



            foreach (var item in existBook.BookImages.Where(x => !book.BookImageIds.Contains(x.Id) && x.isPoster == null))
            {
                string fullPath = Path.Combine(_env.WebRootPath, "uploads/books", item.ImgUrl);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == null);

            if (book.ImageFiles != null)
            {
                foreach (var img in book.ImageFiles)
                {

                    if (img.ContentType != "images/png" && img.ContentType != "image/jpeg")
                    {
                        throw new InvalidContentType("ImageFiles", "please select correct file type");
                    }

                    if (img.Length > 1048576)
                    {
                        throw new InvalidImageSize("ImageFiles", "please select correct file type");
                    }

                    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", img);
                    BookImage bookImage = new BookImage
                    {
                        Book = book,
                        ImgUrl = newFileName,
                        isPoster = null,
                    };
                    existBook.BookImages.Add(bookImage);
                }
            }


            existBook.Name = book.Name;
            existBook.Desc = book.Desc;
            existBook.Tax = book.Tax;
            existBook.Code = book.Code;
            existBook.SalePrice = book.SalePrice;
            existBook.CostPrice = book.CostPrice;
            existBook.IsAvailable = book.IsAvailable;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;


            await _bookRepository.SaveAsync();
        }
    }
}
