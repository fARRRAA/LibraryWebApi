using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using LibraryWebApi.Interfaces;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {

        public Check Check;
        private readonly IGenreService _genre;

        public GenreController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor,IGenreService genre)
        {

            Check = new Check(httpContextAccessor);
            _genre = genre;
        }
        [HttpGet]
        [Route("getAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {

            return new OkObjectResult(new
            {
                genres = _genre.GetAllGenres()
            });
        }
        [Authorize]
        [HttpPost]
        [Route("addNewGenre")]
        public async Task<IActionResult> AddNewGenre([FromQuery] CreateGenre createdGenre)
        {
            var admin = Check.IsUserAdmin();
            if (admin != "admin")
            {
                return new UnauthorizedObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")

                });
            }
            if (_genre.GetAllGenres().Any(g=>g.Name== createdGenre.Name))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that name already exists")
                });
            }
            await _genre.AddNewGenre(createdGenre);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("updateGenreById/{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, [FromQuery] CreateGenre createdGenre)
        {
            var admin = Check.IsUserAdmin();
            if (admin != "admin")
            {
                return new UnauthorizedObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new BadRequestObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            if (!_genre.GenreExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });
            }
            await _genre.UpdateGenreById(id, createdGenre);
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteGenreById/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            var admin = Check.IsUserAdmin();
            if (admin != "admin")
            {
                return new UnauthorizedObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_genre.GenreExists(id))
            {
                return new NotFoundObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });
            }
            await _genre.DeleteGenreById(id);
            return Ok();
        }
    }
}
