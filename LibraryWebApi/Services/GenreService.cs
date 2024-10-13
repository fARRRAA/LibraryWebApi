using LibraryWebApi.Controllers;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{
    public class GenreService : IGenreService
    {
        readonly LibraryWebApiDb _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Check Check;


        public GenreService(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor, Check check)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Check = check;
        }
        public async Task AddNewGenre(CreateGenre createdGenre)
        {
            var check =  _context.Genre.FirstOrDefaultAsync(i => i.Name.ToLower() == createdGenre.Name.ToLower());
            var genre = new Genre() { Name = createdGenre.Name };
            await _context.Genre.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenreById(int id)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            _context.Genre.Remove(genre);
            _context.SaveChanges();
        }

        public List<Genre> GetAllGenres()
        {
            return _context.Genre.ToList();
        }

        public async Task UpdateGenreById(int id, CreateGenre createdGenre)
        {
            var genre = await _context.Genre.FirstOrDefaultAsync(i => i.Id_Genre == id);
            genre.Name = createdGenre.Name;
            await _context.SaveChangesAsync();
        }

        public bool GenreExists(int id)
        {
            return _context.Genre.Any(g => g.Id_Genre == id);

        }

    }
}
