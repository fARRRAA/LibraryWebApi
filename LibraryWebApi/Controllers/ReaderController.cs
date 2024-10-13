using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LibraryWebApi.Interfaces;
using System.Reflection.PortableExecutable;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Check Check;
        private readonly IReaderService _reader;
        public ReaderController(IHttpContextAccessor httpContextAccessor, IReaderService readerService)
        {
            _httpContextAccessor = httpContextAccessor;
            Check = new Check(httpContextAccessor);
            _reader = readerService;
        }
        [Authorize]
        [HttpGet("getAllReaders")]
        public async Task<ActionResult> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
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
                readers = _reader.GetAllReaders(page, pageSize)
            });
        }
        [Authorize]
        [HttpPost("addNewReader")]
        public async Task<IActionResult> AddNewReader([FromQuery]createReader reader)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (_reader.ReaderExists(reader.Login))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader with that login and password already exists")
                });
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            await _reader.AddNewReader(reader);
            return Ok();
        }

        [Authorize]
        [HttpPut("updateReaderById/{id}")]
        public async Task<IActionResult> UpdateReaderById(int id, [FromQuery]createReader reader)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_reader.GetAll().Any(r => r.Id_User == id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return new OkObjectResult(new
                {
                    error = BadRequest("fill in all fields")
                });
            }
            await _reader.UpdateReaderById(id,reader);
            return Ok();
        }
        [Authorize]
        [HttpDelete("deleteReaderById/{id}")]
        public async Task<IActionResult> DeleteReaderById(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_reader.GetAll().Any(r => r.Id_User == id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }
            await _reader.DeleteReaderById(id);
            return Ok();
        }
        [Authorize]
        [HttpGet("getReaderById{id}")]
        public async Task<IActionResult> GetReaderById(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("only admin could do this")
                });
            }
            if (!_reader.GetAll().Any(r => r.Id_User == id))
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader with that id does not exists")
                });
            }

            return new OkObjectResult(new
            {
                reader = _reader.GetReaderById(id)
            });
        }
        //[Authorize]
        //[HttpGet("getReadersBooks/{id}")]
        //public async Task<IActionResult> GetReadersRentals(int id)
        //{
        //    var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
        //    if (check == null)
        //    {
        //        return new OkObjectResult(new
        //        {
        //            error = NotFound("reader with that id don`t exists")
        //        });
        //    }
        //    var checkRents = await _context.RentHistory.FirstOrDefaultAsync(r => r.Id_Reader == id);
        //    if (checkRents == null)
        //    {
        //        return new OkObjectResult(new
        //        {
        //            error = NotFound("reader has no rentals")
        //        });
        //    }
        //    var bookIds = await _context.RentHistory.Where(r => r.Id_Reader == id).Select(r => r.Id_Book).ToListAsync();
        //    var books = await _context.Books.Where(b => bookIds.Contains(b.Id_Book)).ToListAsync();
        //    return new OkObjectResult(new
        //    {
        //        books = books
        //    });
        //}
        [HttpGet("isAdmin")]
        public async Task<IActionResult> checkRole()
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return new OkObjectResult(new
                {
                    error = Unauthorized("you not admin")
                });
            }
            return Ok("you admin");
        }
    }
}
