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
        //public Check Check;
        //private readonly IGenreService _genre;
        //public GenreController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor,IGenreService genre)
        //{
        //    Check = new Check(httpContextAccessor);
        //    _genre = genre;
        //}
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllGenres()
        {
            return null;
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewGenre([FromQuery] CreateGenre createdGenre)
        {
            return null;
        }
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateGenreById(int id, [FromQuery] CreateGenre createdGenre)
        {
            return null;
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            return null;
        }
    }
}
