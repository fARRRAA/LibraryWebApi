using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [HttpPost]
        [Route("addNewGenre")]
        public async Task<IActionResult> AddNewGenre(CreateGenre createdGenre)
        {
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return BadRequest("fill in all fields");
            }
            var check = await _context.Genre.FirstOrDefaultAsync(i => i.Name.ToLower() == createdGenre.Name.ToLower());
            if (check != null)
            {
                return BadRequest("genre with that name already exists");
            }
            var genre = new Genre() { Name = createdGenre.Name };
            await _context.Genre.AddAsync(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpPut]
        [Route("updateGenreById/{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, CreateGenre createdGenre)
        {
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return BadRequest("fill in all fields");
            }
            var  genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            if (genre == null)
            {
                return BadRequest("genre with that id do not exists");
            }
            genre.Name= createdGenre.Name;
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
        [HttpDelete]
        [Route("deleteGenreById/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            if (genre == null)
            {
                return BadRequest("genre with that id do not exists");
            }
            _context.Genre.Remove(genre);
            _context.SaveChanges();
            return Ok();
        }
    }
}
