using LibraryWebApi.Model;
using LibraryWebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Interfaces
{
    public interface IReaderService
    {
        public List<Readers> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize);
        public Task AddNewReader(createReader reader);
        public Readers GetReaderById(int id);
        public Task UpdateReaderById(int id, createReader reader);
        public Task DeleteReaderById(int id);
        public List<RentHistory> GetReadersRentals(int id);
        public bool ReaderExists(string login);
        public List<Readers> GetAll();

    }
}
