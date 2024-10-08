﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Check Check;

        public GenreController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Check = new Check(httpContextAccessor);
        }
        [HttpGet]
        [Route("getAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _context.Genre.ToListAsync();
            if (genres == null)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("could not find genres")
                });
            }
            return new OkObjectResult(new
            {
                genres = genres
            });
        }
        [Authorize]
        [HttpPost]
        [Route("addNewGenre")]
        public async Task<IActionResult> AddNewGenre(CreateGenre createdGenre)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });

            }
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")

                });
            }
            var check = await _context.Genre.FirstOrDefaultAsync(i => i.Name.ToLower() == createdGenre.Name.ToLower());
            if (check != null)
            {
                return new OkObjectResult(new
                {
                    error= NotFound("genre with that name already exists")
                });
            }
            var genre = new Genre() { Name = createdGenre.Name };
            await _context.Genre.AddAsync(genre);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpPut]
        [Route("updateGenreById/{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, CreateGenre createdGenre)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });

            }
            if (string.IsNullOrWhiteSpace(createdGenre.Name))
            {
                return new OkObjectResult(new 
                {
                    error=BadRequest("fill in all fields")
                });
            }
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            if (genre == null)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });
            }
            genre.Name = createdGenre.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        [Route("deleteGenreById/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });

            }
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            if (genre == null)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("genre with that id do not exists")
                });            }
            _context.Genre.Remove(genre);
            _context.SaveChanges();
            return Ok();
        }
    }
}
