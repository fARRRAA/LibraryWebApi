using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace LibraryWebApi.Controllers
{

    public class AuthController : Controller
    {
        readonly LibraryWebApiDb _context;
        private string key = "secretkeyildarsecretkeyildarsecretkeyildar";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("registerReader")]
        public async Task<ActionResult> Register(createReader reader)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Login == reader.Login && r.Password == reader.Password);
            if (check != null)
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
            var hashedPassword = Generate(reader.Password);

            var Reader = new Readers()
            {
                Name = reader.Name,
                Password = reader.Password,
                Date_Birth = reader.Date_Birth,
                Login = reader.Login,
                Id_Role = 2
            };
            await _context.Readers.AddAsync(Reader);
            await _context.SaveChangesAsync();
            return new OkObjectResult(new
            {
                reader = reader
            });
        }
        [HttpGet("loginReader")]
        public async Task<ActionResult> Login(string login, string password)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Login == login);
            if (check == null)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("reader not found")
                });
            }
            //var res = Verify(password, check.Password);
            if (password != check.Password)
            {
                return new OkObjectResult(new
                {
                    error = NotFound("wrong password")
                });
            }
            var token = GenerateToken(check);
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Headers.Add("Authorization", $"{token}");
            return new OkObjectResult(new
            {
                token = token
            });
        }



        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public bool Verify(string password, string hashPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);

        public string GenerateToken(Readers reader)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Authentication, reader.Id_User.ToString()),
                new(ClaimTypes.Role, reader.Id_Role switch { 1 => "admin", 2 => "reader" })
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(6)
                );
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
