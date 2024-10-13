using LibraryWebApi.Controllers;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{
    public class RentService( LibraryWebApiDb _context) : IRentService
    {

        public Task<IActionResult> RentBookById(int id, int readerId, int rentalTime)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> ReturnRent(int rentId)
        {
            throw new NotImplementedException();
        }
        public List<RentHistory> GetBookRentals(int id)
        {
            return _context.RentHistory.Where(h => h.Id_Book == id).Include(h=>h.Book).ToList();
        }

        public List<RentHistory> GetCurrentRentals()
        {
            return _context.RentHistory.Where(h=>h.Rental_Status=="нет").Include(h=>h.Book).Include(h=>h.Reader).ToList();
        }

        public List<RentHistory> GetReadersRentals(int id)
        {
            throw new NotImplementedException();
        }
        public bool BookInRentExists(int bookId)
        {
            return _context.RentHistory.Any(r=>r.Id_Book==bookId);
        }
        public bool RentExists(int id)
        {
            return _context.RentHistory.Any(r=>r.id_Rent==id);
        }

    }
}
