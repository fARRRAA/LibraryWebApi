using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Model
{
    public class Genre
    {
        [Key]
        public int Id_Genre { get; set; }
        public string Name { get; set; }
    }
}
