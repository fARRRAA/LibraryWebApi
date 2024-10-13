using LibraryWebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Interfaces
{
    public interface IRentService
    {
        public Task<IActionResult> RentBookById(int id, int readerId, int rentalTime);
        public List<RentHistory> GetReadersRentals(int id);
        public Task<IActionResult> ReturnRent(int rentId);
        public List<RentHistory> GetCurrentRentals();

        public List<RentHistory> GetBookRentals(int id);
        public bool BookInRentExists(int bookId);
        public bool RentExists(int id);


    }
}
