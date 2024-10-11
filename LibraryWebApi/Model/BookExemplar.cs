using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace LibraryWebApi.Model
{
    public class BookExemplar
    {
        [Key]
        public int Exemplar_Id { get; set; }
        public int Books_Count { get; set; }

        [ForeignKey(nameof(Book))]
        public int Book_Id { get; set; }

        public Books Book { get; set; }
        public int Exemplar_Price { get; set; }
    }
}
