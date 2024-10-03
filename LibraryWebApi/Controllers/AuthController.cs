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
                return NotFound("reader with that login and password already exists");
            }
            if (string.IsNullOrWhiteSpace(reader.Name) || string.IsNullOrWhiteSpace(reader.Password) || string.IsNullOrWhiteSpace(reader.Login) || string.IsNullOrWhiteSpace(reader.Date_Birth.ToString()))
            {
                return NotFound("fill in all fields");
            }
            var hashedPassword = Generate(reader.Password);

            var Reader = new Readers()
            {
                Name = reader.Name,
                Password = hashedPassword,
                Date_Birth = reader.Date_Birth,
                Login = reader.Login,
                Id_Role = 2
            };
            await _context.Readers.AddAsync(Reader);
            await _context.SaveChangesAsync();
            return Ok(Reader);


        }
        [HttpGet("loginReader")]
        public async Task<ActionResult> Login(string login, string password)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Login == login);
            if (check == null)
            {
                return NotFound("reader not found");
            }
            var res = Verify(password, check.Password);
            if (res == false)
            {
                return NotFound("wrong password");
            }
            var token = GenerateToken(check);
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("wild-cookies", token);
            return Ok(token);

        }
        public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        public bool Verify(string password, string hashPassword) => BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);

        public string GenerateToken(Readers reader)
        {
            Claim[] claims = [new("userId", reader.Id_User.ToString())];

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
