using Microsoft.AspNetCore.Mvc;
using LibraryWebApi.Requests;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        //private string URL = "https://localhost:7018/api/Books/";
        //private readonly IBookService _books;
        //private readonly IGenreService _genre;
        //public Check Check;
        //private readonly HttpClient _httpClient;
        //public BooksController(IBookService bookService, Check check, IGenreService genreService)
        //{
        //    _books = bookService;
        //    Check = check;
        //    _genre = genreService;
        //    _httpClient = new HttpClient();
        //}
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? author, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] int? page,
        [FromQuery] int? pageSize)
        {
            return null;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewBook([FromBody] CreateBook book)
        {
            return null;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] CreateBook book)
        {
            return null;
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            return null;
        }
        [HttpGet]
        [Route("genre/{id}")]
        public async Task<IActionResult> GetBooksByGenre(int id)
        {
            return null;
        }
        [HttpGet]
        [Route("author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string author)
        {
            return null;
        }
        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetBooksByName(string name, int? page, int? pageSize)
        {
            return null;
        }
        [HttpGet]
        [Route("exemplars")]
        public async Task<IActionResult> GetALlExemplars()
        {
            return null;
        }
        [HttpGet]
        [Route("exemplar/{bookId}")]
        public async Task<IActionResult> GetExemplar(int bookId)
        {
            return null;
        }
        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            return null;
        }
    }
}
