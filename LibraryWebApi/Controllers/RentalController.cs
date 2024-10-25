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
        //public Check Check;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IBookService _book;
        //private readonly IReaderService _reader;
        //private readonly IRentService _rent;
        //private readonly IBookExemplarService _exemplar;
        //public RentalController(IReaderService reader, IBookService book, LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor, IRentService rentService,IBookExemplarService exemplar)
        //{
        //    Check = new Check(httpContextAccessor);
        //    _reader = reader;
        //    _book = book;
        //    _rent = rentService;
        //    _exemplar = exemplar;
        //}
        //[Authorize]
        [HttpPost("RentBookById/{bookId}")]
        public async Task<IActionResult> RentBookById(int bookId, int readerId, int rentalTime)
        {
            return null;
        }
        [HttpGet("getReadersRentals/{id}")]
        public async Task<IActionResult> GetReadersRentals(int id)
        {
            return null;
        }
        [HttpPost("returnRent{rentId}")]
        public async Task<IActionResult> ReturnRent(int rentId)
        {
            return null;
        }
        [HttpGet("getCurrentRentals")]
        public async Task<IActionResult> GetCurrentRentals()
        {
            return null;
        }
        [HttpGet("getBookRentals/{id}")]
        public async Task<IActionResult> GetBookRentals(int id)
        {
            return null;
        }
    }
}
