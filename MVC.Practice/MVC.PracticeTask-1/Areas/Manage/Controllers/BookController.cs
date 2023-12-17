using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVC.Practice.PustokMVC.Business.Exceptions.BookExceptions;
using MVC.Practice.PustokMVC.Business.Exceptions.CommonModelsExceptions;
using MVC.Practice.PustokMVC.Business.Services;
using MVC.Practice.PustokMVC.Core.Models;
using MVC.PracticeTask_1.Pagination;

namespace MVC.PracticeTask_1.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {

        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly ITagService _tagService;
        public BookController(IBookService bookService,
                              IAuthorService authorService,
                              IGenreService genreService,
                              ITagService tagService
            )
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _tagService = tagService;

        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _bookService.GetBookTable().Include(a => a.Author).Include(g => g.Genre);

            PaginatedList<Book> paginatedBooks = PaginatedList<Book>.Create(query, page, 3);

            return View(paginatedBooks);
        }
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Genres = await _genreService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Genres = await _genreService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            if (!ModelState.IsValid) return View();

            #region oldcodes

            //ViewBag.Authors = _DbContext.Authors.ToList();
            //ViewBag.Genres = _DbContext.Genres.ToList();
            //ViewBag.Tags = _DbContext.Tags.ToList();


            //if (!_DbContext.Authors.Any(a => a.Id == book.AuthorId))
            //{
            //    ModelState.AddModelError("AuthorId", "Author is not found!");
            //    return View();
            //}

            //if (!_DbContext.Genres.Any(g => g.Id == book.GenreId))
            //{
            //    ModelState.AddModelError("GenreId", "Genre is not found!");
            //    return View();
            //}

            //bool check = false;

            //if (book.TagIds != null)
            //{
            //    foreach (var tagId in book.TagIds)
            //    {
            //        if (!_DbContext.Tags.Any(t => t.Id == tagId))
            //        {
            //            check = true;
            //            break;
            //        }

            //    }
            //}

            //if (check)
            //{
            //    ModelState.AddModelError("TagId", "Tag id not found!");
            //    return View();
            //}
            //else
            //{
            //    if (book.TagIds != null)
            //    {
            //        foreach (var tagId in book.TagIds)
            //        {
            //            BookTag bookTag = new BookTag()
            //            {
            //                Book = book,
            //                TagId = tagId,
            //            };

            //            _DbContext.BookTags.Add(bookTag);
            //        }
            //    }
            //}


            //if (book.BookMainImage != null)
            //{

            //    if (book.BookMainImage.ContentType != "image/png" && book.BookMainImage.ContentType != "image/jpeg")
            //    {
            //        ModelState.AddModelError("BookMainImage", "please select correct file type");
            //        return View();
            //    }

            //    if (book.BookMainImage.Length > 1048576)
            //    {
            //        ModelState.AddModelError("BookMainImage", "file size should be more lower than 1mb ");
            //        return View();
            //    }

            //    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookMainImage);
            //    BookImage bookImage = new BookImage
            //    {
            //        Book = book,
            //        ImgUrl = newFileName,
            //        isPoster = true,
            //    };
            //    _DbContext.BookImages.Add(bookImage);
            //};


            //if (book.BookHoverImage != null)
            //{

            //    if (book.BookHoverImage.ContentType != "image/png" && book.BookHoverImage.ContentType != "image/jpeg")
            //    {
            //        ModelState.AddModelError("BookHoverImage", "please select correct file type");
            //        return View();
            //    }

            //    if (book.BookHoverImage.Length > 1048576)
            //    {
            //        ModelState.AddModelError("BookHoverImage", "file size should be more lower than 1mb ");
            //        return View();
            //    }

            //    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookHoverImage);
            //    BookImage bookImage = new BookImage
            //    {
            //        Book = book,
            //        ImgUrl = newFileName,
            //        isPoster = false,
            //    };
            //    _DbContext.BookImages.Add(bookImage);
            //};

            //if (book.ImageFiles != null)
            //{
            //    foreach (var img in book.ImageFiles)
            //    {
            //        string fileName = img.FileName;
            //        if (img.ContentType != "image/png" && img.ContentType != "image/jpeg")
            //        {
            //            ModelState.AddModelError("ImageFiles", "please select correct file type");
            //            return View();
            //        }

            //        if (img.Length > 1048576)
            //        {
            //            ModelState.AddModelError("ImageFiles", "file size should be more lower than 1mb ");
            //            return View();
            //        }

            //        string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", img);
            //        BookImage bookImage = new BookImage
            //        {
            //            Book = book,
            //            ImgUrl = newFileName,
            //            isPoster = null,
            //        };
            //        _DbContext.BookImages.Add(bookImage);
            //    }
            //}

            //_DbContext.Books.Add(book);
            //_DbContext.SaveChanges();
            #endregion
            try
            {
                await _bookService.CreateAsync(book);
            }
            catch (InvalidContentTypeOrImageSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (NotFound ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Genres = await _genreService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            if (id == null) return NotFound();

            try
            {
                await _bookService.SoftDelete(id);
            }
            catch (InvalidNullReferance)
            {

            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Genres = await _genreService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            if (id == null) return NotFound();

            Book book = await _bookService.GetByIdAsync(id);

            if (book == null) return NotFound();

            book.TagIds = book.BookTags.Select(t => t.TagId).ToList();


            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Book book)
        {

            ViewBag.Authors = await _authorService.GetAllAsync();
            ViewBag.Genres = await _genreService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();


            #region oldcodes
            //Book existBook = _DbContext.Books.Include(bt => bt.BookTags).Include(x => x.BookImages).FirstOrDefault(b => b.Id == book.Id);

            //if (existBook == null) return NotFound();

            //if (!_DbContext.Authors.Any(a => a.Id == book.AuthorId))
            //{
            //    ModelState.AddModelError("AuthorId", "Author is not found!");
            //    return View();
            //}

            //if (!_DbContext.Genres.Any(g => g.Id == book.GenreId))
            //{
            //    ModelState.AddModelError("GenreId", "Genre is not found!");
            //    return View();
            //}

            //existBook.BookTags.RemoveAll(bt => !book.TagIds.Any(tId => tId == bt.TagId));

            //foreach (var id in book.TagIds.Where(bt => !existBook.BookTags.Any(tId => bt == tId.TagId)))
            //{
            //    BookTag bookTag = new BookTag()
            //    {
            //        TagId = id,
            //    };

            //    existBook.BookTags.Add(bookTag);
            //}


            //if (book.BookMainImage != null)
            //{
            //    string folderPath = "uploads/books";
            //    string path = Path.Combine(_env.WebRootPath, folderPath, existBook.BookImages.FirstOrDefault(x => x.isPoster == true).ImgUrl);

            //    existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == true);


            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }

            //    if (book.BookMainImage.ContentType != "image/png" && book.BookMainImage.ContentType != "image/jpeg")
            //    {
            //        ModelState.AddModelError("BookMainImage", "please select correct file type");
            //        return View();
            //    }

            //    if (book.BookMainImage.Length > 1048576)
            //    {
            //        ModelState.AddModelError("BookMainImage", "file size should be more lower than 1mb ");
            //        return View();
            //    }

            //    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookMainImage);
            //    BookImage bookImage = new BookImage
            //    {
            //        Book = book,
            //        ImgUrl = newFileName,
            //        isPoster = true,
            //    };
            //    existBook.BookImages.Add(bookImage);
            //};

            //if (book.BookHoverImage != null)
            //{
            //    string folderPath = "uploads/books";
            //    string path = Path.Combine(_env.WebRootPath, folderPath, existBook.BookImages.Where(x => x.isPoster == false).FirstOrDefault().ImgUrl);
            //    existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == false);


            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }

            //    if (book.BookHoverImage.ContentType != "image/png" && book.BookHoverImage.ContentType != "image/jpeg")
            //    {
            //        ModelState.AddModelError("BookHoverImage", "please select correct file type");
            //        return View();
            //    }

            //    if (book.BookHoverImage.Length > 1048576)
            //    {
            //        ModelState.AddModelError("BookHoverImage", "file size should be more lower than 1mb ");
            //        return View();
            //    }

            //    string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", book.BookHoverImage);
            //    BookImage bookImage = new BookImage
            //    {
            //        Book = book,
            //        ImgUrl = newFileName,
            //        isPoster = false,
            //    };
            //    existBook.BookImages.Add(bookImage);
            //};



            //foreach (var item in existBook.BookImages.Where(x => !book.BookImageIds.Contains(x.Id) && x.isPoster == null))
            //{
            //    string fullPath = Path.Combine(_env.WebRootPath, "uploads/books", item.ImgUrl);

            //    if (System.IO.File.Exists(fullPath))
            //    {
            //        System.IO.File.Delete(fullPath);
            //    }
            //}

            //existBook.BookImages.RemoveAll(bi => !book.BookImageIds.Contains(bi.Id) && bi.isPoster == null);

            //if (book.ImageFiles != null)
            //{
            //    foreach (var img in book.ImageFiles)
            //    {

            //        if (img.ContentType != "images/png" && img.ContentType != "image/jpeg")
            //        {
            //            ModelState.AddModelError("ImageFiles", "please select correct file type");
            //            return View();
            //        }

            //        if (img.Length > 1048576)
            //        {
            //            ModelState.AddModelError("ImageFiles", "file size should be more lower than 1mb ");
            //            return View();
            //        }

            //        string newFileName = Helper.GetFileName(_env.WebRootPath, "uploads/books", img);
            //        BookImage bookImage = new BookImage
            //        {
            //            Book = book,
            //            ImgUrl = newFileName,
            //            isPoster = null,
            //        };
            //        existBook.BookImages.Add(bookImage);
            //    }
            //}


            //existBook.Name = book.Name;
            //existBook.Desc = book.Desc;
            //existBook.Tax = book.Tax;
            //existBook.Code = book.Code;
            //existBook.SalePrice = book.SalePrice;
            //existBook.CostPrice = book.CostPrice;
            //existBook.IsAvailable = book.IsAvailable;
            //existBook.DiscountPercent = book.DiscountPercent;
            //existBook.AuthorId = book.AuthorId;
            //existBook.GenreId = book.GenreId;

            //_DbContext.SaveChanges();
            #endregion
            try
            {
                await _bookService.UpdateAsync(book);
            }
            catch (InvalidContentTypeOrImageSize ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (NotFound ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (NullReferenceException ex)
            {

            }

            return RedirectToAction("Index");
        }
    }
}
