using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
//using LibraryWebApi.Requests;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        readonly LibraryWebApiDb _context;

        public BooksController(LibraryWebApiDb context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _context.Books.ToListAsync();
            return new OkObjectResult(new
            {
                books = books,
                status=true

            });
        }
    }
}
