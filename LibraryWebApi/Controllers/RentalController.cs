using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Controllers
{
    public class RentalController : Controller
    {
        readonly LibraryWebApiDb _context;
        public RentalController(LibraryWebApiDb context)
        {
            _context = context;
        }

        [HttpPost("RentBookById/{id}")]
        public async Task<IActionResult> RentBookById(int id, int readerId, int rentalTime)
        {
            if(string.IsNullOrWhiteSpace(Convert.ToString(readerId))|| string.IsNullOrWhiteSpace(Convert.ToString(rentalTime)))
            {
                return BadRequest("fill in all fields");
            }
            var checkBook = await _context.Books.FirstOrDefaultAsync(b => b.Id_Book == id);
            if (checkBook == null)
            {
                return NotFound("could not find book with that id");
            }
            var checkReader = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == readerId);
            if (checkReader == null)
            {
                return NotFound("could not find reader with that id");

            }
            var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkBook.Id_Book);

            if (bookExemplar == null || bookExemplar.Books_Count == 0)
            {
                return BadRequest("book with that id has 0 exemplars");
            }
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

            return Ok(rental);
        }
        [HttpGet("getReadersRentals/{id}")]
        public async Task<IActionResult> GetReadersRentals(int id)
        {
            var check = await _context.RentHistory.FirstOrDefaultAsync(r => r.Id_Reader == id);
            if (check == null)
            {
                return NotFound("this reader has no rentals");
            }
            return Ok(_context.RentHistory.Where(r => r.Id_Reader == check.Id_Reader));
        }
    }
}
