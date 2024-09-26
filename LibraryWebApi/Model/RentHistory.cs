using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Model
{
    public class RentHistory
    {
        [Key]
        public int id_Rent { get; set; }
        public DateTime Rental_Start { get; set; }
        public int Rental_Time { get; set; }
        [Required]
        [ForeignKey("Readers")]
        public int Id_Reader { get; set; }
        [Required]
        [ForeignKey(name: "Books")]
        public int Id_Book { get; set; }

        //public Readers Readers {  get; set; }
        //public Books Books { get; set; }

    }
}
