using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : Controller
    {
        public Check Check;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookService _book;
        private readonly IReaderService _reader;
        private readonly IRentService _rent;
        public RentalController(IReaderService reader, IBookService book, LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor, IRentService rentService)
        {
            Check = new Check(httpContextAccessor);
            _reader = reader;
            _book = book;
            _rent = rentService;

        }
        [Authorize]
        [HttpPost("RentBookById/{id}")]
        public async Task<IActionResult> RentBookById(int bookId, int readerId, int rentalTime)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(readerId)) || string.IsNullOrWhiteSpace(Convert.ToString(rentalTime)))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            if (_rent.GetCurrentRentals().Any(r => r.Id_Reader == readerId && r.Id_Book == bookId))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("you already rent this book")
                });

            }
            if (!_book.BookExists(bookId))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("not found book with this id")
                });
            }
            if (!_reader.GetAll().Any(r => r.Id_User == bookId))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }

            if (bookExemplar == null || bookExemplar.Books_Count == 0)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("book with that id has 0 exemplars")
                });
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

            return Ok();
        }
        //[Authorize]
        //[HttpGet("getReadersRentals/{id}")]
        //public async Task<IActionResult> GetReadersRentals(int id)
        //{
        //    var check = await _context.RentHistory.FirstOrDefaultAsync(r => r.Id_Reader == id);
        //    if (check == null)
        //    {
        //        return new OkObjectResult(new
        //        {
        //            error = NotFound("reader has no rentals")
        //        });
        //    }
        //    return new OkObjectResult(new
        //    {
        //        rentals = _context.RentHistory.Where(r => r.Id_Reader == check.Id_Reader)
        //    });
        //}
        //[Authorize]
        //[HttpPost("returnRent{rentId}")]
        //public async Task<IActionResult> ReturnRent(int rentId)
        //{
        //    var checkRent = await _context.RentHistory.FirstOrDefaultAsync(r => r.id_Rent == rentId);
        //    if (checkRent == null)
        //    {
        //        return new OkObjectResult(new
        //        {
        //            error = NotFound("not found rent with this id")
        //        });
        //    }
        //    var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkRent.Id_Book);
        //    checkRent.Rental_Status = "да";
        //    bookExemplar.Books_Count += 1;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
        [Authorize]
        [HttpGet("getCurrentRentals")]
        public async Task<IActionResult> GetCurrentRentals()
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });

            }
            return new OkObjectResult(new
            {
                rentals = _rent.GetCurrentRentals()
            });
        }
        [Authorize]
        [HttpGet("getBookRentals/{id}")]
        public async Task<IActionResult> GetBookRentals(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });

            }
            if (!_book.BookExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("not found book with this id")
                });
            }
            if (!_rent.BookInRentExists(id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("not found rent with this book id")
                });
            }
            return new OkObjectResult(new
            {
                rentals = _rent.GetBookRentals(id)
            });
        }
    }
}
