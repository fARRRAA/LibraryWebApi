using Microsoft.EntityFrameworkCore;
using LibraryWebApi.Model;
namespace LibraryWebApi.DataBaseContext
{
    public class LibraryWebApiDb : DbContext
    {

        public LibraryWebApiDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Readers> Readers { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<RentHistory> RentHistory { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}
