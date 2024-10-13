using LibraryWebApi.Controllers;
using LibraryWebApi.DataBaseContext;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Services
{

    public class ReaderService(LibraryWebApiDb context, IHttpContextAccessor httpContextAccessor, Check check) : IReaderService
    {
        readonly LibraryWebApiDb _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public Check Check = check;
        public List<Readers> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize)
        {
            var users = _context.Readers;
            var totalUsers =  users.Count();
            if (page.HasValue && pageSize.HasValue)
            {
                var usersPaginated =  users.Skip((int)((page - 1) * (int)pageSize)).Take((int)pageSize).ToList();
                return usersPaginated;
            }
            return users.ToList();
            
        }

        public async Task AddNewReader([FromQuery]createReader reader)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Login == reader.Login);
            var Reader = new Readers
            {
                Name = reader.Name,
                Password = reader.Password,
                Date_Birth = reader.Date_Birth,
                Login = reader.Login,
                Id_Role = 2
            };
            await _context.Readers.AddAsync(Reader);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReaderById(int id)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User == id);
            _context.Readers.Remove(check);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReaderById(int id, [FromQuery]createReader reader)
        {
            var check = await _context.Readers.FirstOrDefaultAsync(r => r.Id_User==id);
            check.Name = reader.Name;
            check.Password = reader.Password;
            check.Date_Birth = reader.Date_Birth;
            check.Login = reader.Login;
            await _context.SaveChangesAsync();
        }

        public Readers GetReaderById(int id)
        {
            return _context.Readers.FirstOrDefault(r=>r.Id_User==id);
        }

        public List<RentHistory> GetReadersRentals(int id)
        {
            throw new NotImplementedException();
        }

        public bool ReaderExists(string login)
        {
            return _context.Readers.Any(r => r.Login == login);
        }
        public List<Readers> GetAll()
        {
            return _context.Readers.ToList();
        }
    }
}
