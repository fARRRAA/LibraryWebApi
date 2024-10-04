using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Authorization;
namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        readonly LibraryWebApiDb _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Check Check;

        public ReaderController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            Check = new Check(httpContextAccessor);
        }

        [Authorize]
        [HttpGet("getAllReaders")]
        public async Task<ActionResult> GetAllReaders()
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var users = _context.Readers.ToListAsync();
            return new OkObjectResult(new
            {
                Readers = users

            });
        }
        [Authorize]
        [HttpPost("addNewReader")]
        public async Task<IActionResult> AddNewReader(createReader reader)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Login == reader.Login);
            if (check != null)
            {
                return BadRequest("reader with that login and password already exists");
            }
            if(string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password)|| string.IsNullOrWhiteSpace(reader.Login)|| string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return BadRequest("fill in all fields");
            }
            var Reader = new Readers()
            {
                Name=reader.Name,
                Password=reader.Password,
                Date_Birth=reader.Date_Birth,
                Login=reader.Login,
                Id_Role=2
            };
            await _context.Readers.AddAsync(Reader);
            await _context.SaveChangesAsync();
            return Ok(Reader);
        }
        [Authorize]
        [HttpGet("getReaderById{id}")]
        public async Task<IActionResult> GetReaderById(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
            if (check == null)
            {
                return BadRequest("reader with that id don`t exists");
            }
            return Ok(check);
        }
        [Authorize]
        [HttpPut("updateReaderById/{id}")]
        public async Task<IActionResult> UpdateReaderById(int id, createReader reader)
        {

            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
            if (check == null)
            {
                return BadRequest("reader with that id don`t exists");
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return BadRequest("fill in all fields");
            }
            check.Name = reader.Name;
            check.Password = reader.Password;
            check.Date_Birth = reader.Date_Birth;
            check.Login = reader.Login;
            await _context.SaveChangesAsync();
            return Ok(check);
        }
        [Authorize]
        [HttpDelete("deleteReaderById/{id}")]
        public async Task<IActionResult> DeleteReaderById(int id)
        {
            bool admin = Check.IsUserAdmin();
            if (!admin)
            {
                return Unauthorized("only admin could do this");
            }
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
            if (check == null)
            {
                return BadRequest("reader with that id don`t exists");
            }
            _context.Readers.Remove(check);
            _context.SaveChanges();
            return Ok("reader has been successfully removed");
        }
        [Authorize]
        [HttpGet("getReadersBooks/{id}")]
        public async Task<IActionResult> GetReadersRentals(int id)
        {

            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
            if (check == null)
            {
                return NotFound("reader with that id don`t exists");
            }
            var checkRents = await _context.RentHistory.FirstOrDefaultAsync(r=>r.Id_Reader == id);
            if(checkRents == null)
            {
                return NotFound("this reader has no rentals");
            }
            var bookIds = await _context.RentHistory.Where(r=>r.Id_Reader==id).Select(r => r.Id_Book).ToListAsync();
            var books = await _context.Books.Where(b => bookIds.Contains(b.Id_Book)).ToListAsync();
            return Ok(books);
        }
    }
}


