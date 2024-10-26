using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : Controller
    {
        [HttpGet("all")]

        public async Task<IActionResult> GetPhotos()
        {
            return null;

        }
        [HttpGet("photo/{name}")]
        public async Task<IActionResult> getPhotoByName(string name)
        {
            return null;

        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            return null;
        }

        [HttpDelete("delete/{name}")]
        public async Task<IActionResult> DeletePhoto(string name)
        {
            return null;
        }
        [HttpPut("update/{name}")]
        public async Task<IActionResult> UpdatePhoto(string name, IFormFile file)
        {
            return null;
        }
        [HttpGet("allFromFolder")]
        public async Task<IActionResult> AllFromFolder()
        {
            return null;
        }
    }
}
