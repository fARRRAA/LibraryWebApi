using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        readonly LibraryWebApiDb _context;

        public GenreController(LibraryWebApiDb context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("getAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _context.Genre.ToListAsync();
            return new OkObjectResult(new
            {
                genres = genres,
                status = true

            });
        }
        [HttpGet]
        [Route("getBookbyId")]
        public async Task<IActionResult> GetBookbyId(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound(new { message = $"Книга с ID {id} не найдена." });
                }
                else
                {
                    return new OkObjectResult(book);
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = $"Произошла ошибка на сервере. Попробуйте позже"});
            }



        }
    }
}
