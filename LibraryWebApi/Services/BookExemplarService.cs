using LibraryWebApi.Controllers;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{
    public class BookExemplarService(LibraryWebApiDb context) : IBookExemplarService
    {
       private readonly LibraryWebApiDb _context = context;
        public bool ExemplarExists(int id)
        {
            return _context.BookExemplar.Any(e => e.Book_Id == id);
        }
        public int ExemplarCounts(int id)
        {
            var exemplar = _context.BookExemplar.FirstOrDefault(e => e.Book_Id == id);
            return exemplar.Books_Count;
        }
    }
}
