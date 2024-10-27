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
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public Check Check;
        //private readonly IReaderService _reader;
        //private readonly IRentService _rent;
        //public ReaderController(IHttpContextAccessor httpContextAccessor, IReaderService readerService, IRentService rent)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    Check = new Check(httpContextAccessor);
        //    _reader = readerService;
        //    _rent = rent;
        //}
        //[Authorize]
        [HttpGet("getAllReaders")]
        public async Task<ActionResult> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            return null;
        }
        [HttpPost("addNewReader")]
        public async Task<IActionResult> AddNewReader([FromQuery]createReader reader)
        {
            return null;
        }
        [HttpPut("updateReaderById/{id}")]
        public async Task<IActionResult> UpdateReaderById(int id, [FromQuery]createReader reader)
        {
            return null;
        }
        [HttpDelete("deleteReaderById/{id}")]
        public async Task<IActionResult> DeleteReaderById(int id)
        {
            return null;
        }
        [HttpGet("getReaderById{id}")]
        public async Task<IActionResult> GetReaderById(int id)
        {
            return null;
        }
        [HttpGet("getReadersBooks/{id}")]
        public async Task<IActionResult> GetReadersRentals(int id)
        {
            return null;
        }
        [HttpGet("isAdmin")]
        public async Task<IActionResult> checkRole()
        {
            return null;
        }
        [HttpPost("uploadpfp")]
        public async Task<IActionResult> UploadProfilePhoto(int readerId, IFormFile photo)
        {
            return null;

        }
        [HttpPut("updatepfp/{readerId}")]
        public async Task<IActionResult> UpdateProfilePhoto(int readerId, IFormFile photo)
        {
            return null;

        }

        [HttpDelete("deletepfp/{readerId}")]
        public async Task<IActionResult> DeleteProfilePhoto(int readerId)
        {
            return null;
        }
    }
}
