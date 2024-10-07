using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using LibraryWebApi.Controllers;
//using LibraryWebApi.Requests;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        readonly LibraryWebApiDb _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Check Check;

        public BooksController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Check = new Check(httpContextAccessor);
        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {

            var books = await _context.Books.ToListAsync();
            return new OkObjectResult(new
            {
                books = books,
                status = true

            });
        }
        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async Task<IActionResult> AddNewBook(CreateBook book)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return BadRequest("fill in all fields");
            }
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Title == book.Title && b.Author == book.Author);
            if (temp != null)
            {
                return NotFound("this book is already exists");
            }
            var Book = new Books()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Year = book.Year,
                Id_Genre = book.Id_Genre
            };
            await _context.Books.AddAsync(Book);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, CreateBook book)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            if (temp == null)
            {
                return BadRequest("book with that id don`t exists");
            }
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Id_Genre)) || string.IsNullOrWhiteSpace(book.Description) || string.IsNullOrWhiteSpace(Convert.ToString(book.Year)))
            {
                return BadRequest("fill in all fields");
            }


            temp.Title = book.Title;
            temp.Author = book.Author;
            temp.Description = book.Description;
            temp.Year = book.Year;
            temp.Id_Genre = book.Id_Genre;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {

            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var temp = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            if (temp == null)
            {
                return BadRequest($"book with id {id} don`t exist");
            }
            _context.Books.Remove(temp);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("getBooksByGenre/{id}")]
        public async Task<IActionResult> GetBooksByGenre(int id)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(g => g.Id_Genre == id);
            if (genre == null)
            {
                return BadRequest($"not found genre with id {id}");
            }
            return Ok(_context.Books.Where(i => i.Id_Genre == id));
        }
        [HttpGet]
        [Route("getBooksByAuthor/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {

            var nameParts = author.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = await _context.Books.Where(i => nameParts.All(part => i.Author.ToLower().Contains(part))).ToListAsync();

            return Ok(books);
        }
        [HttpGet]
        [Route("getBooksByName/{name}")]
        public async Task<IActionResult> GetBooksByName(string name)
        {
            var nameParts = name.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var books = await _context.Books.Where(i => nameParts.Any(part => i.Title.ToLower().Contains(part))).ToListAsync();
            return Ok(books);
        }
        [HttpGet]
        [Route("getAllExemplars")]
        public async Task<IActionResult> GetALlExemplars()
        {
            var allCopies = await _context.BookExemplar.ToListAsync();
            return Ok(allCopies);
        }
        [HttpGet]
        [Route("getExemplar/{bookId}")]
        public async Task<IActionResult> GetExemplar(int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(i => i.Id_Book == bookId);
            var exemplarBook = await _context.BookExemplar.FirstOrDefaultAsync(i => i.Book_Id == bookId);
            if (book == null || exemplarBook == null)
            {
                return BadRequest("could not find exemplars of this book");
            }
            return new OkObjectResult(new
            {
                exemplar = exemplarBook,
                book = book
            });

        }
        [Authorize]
        [HttpGet]
        [Route("getBookbyId")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var book = await _context.Books.FirstOrDefaultAsync(b=>b.Id_Book==id);
            if (book == null)
            {
                return NotFound(new { message = $"Книга с ID {id} не найдена." });
            }
            else
            {
                return new OkObjectResult(book);
            }


        }
    }
}
