using LibraryWebApi.Controllers;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{
    public class RentService(LibraryWebApiDb context) : IRentService
    {
        readonly LibraryWebApiDb _context = context;

        public async Task RentBookById(int id, int readerId, int rentalTime)
        {
            var checkBook = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            var checkReader = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == readerId);
            var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkBook.Id_Book);
            var rental = new RentHistory()
            {
                Id_Book = checkBook.Id_Book,
                Id_Reader = checkReader.Id_User,
                Rental_Start = DateTime.Now,
                Rental_Time = rentalTime,
                Rental_End = DateTime.Now.AddDays(rentalTime),
                Rental_Status = "нет"
            };
            await _context.RentHistory.AddAsync(rental);
            bookExemplar.Books_Count -= 1;
            await _context.SaveChangesAsync();
        }

        public async Task ReturnRent(int rentId)
        {
            var checkRent = await _context.RentHistory.FirstOrDefaultAsync(r => r.id_Rent == rentId);
            var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkRent.Id_Book);
            checkRent.Rental_Status = "да";
            bookExemplar.Books_Count += 1;
            await _context.SaveChangesAsync();
        }
        public List<RentHistory> GetBookRentals(int id)
        {
            return _context.RentHistory.Where(h => h.Id_Book == id).Include(h => h.Book).ToList();
        }

        public List<RentHistory> GetCurrentRentals()
        {
            return _context.RentHistory.Where(h => h.Rental_Status == "нет").Include(h => h.Book).Include(h => h.Reader).ToList();
        }

        public List<RentHistory> GetReadersRentals(int id)
        {

            return _context.RentHistory.Where(r => r.Id_Reader == id).Include(h => h.Book).Include(h => h.Reader).ToList();
        }
        public bool BookInRentExists(int bookId)
        {
            return _context.RentHistory.Any(r => r.Id_Book == bookId);
        }
        public bool RentExists(int id)
        {
            return _context.RentHistory.Any(r => r.id_Rent == id);
        }
        public bool ReaderInRent(int id)
        {
            return _context.RentHistory.Any(r=>r.Id_Reader==id);
        }
    }
}
