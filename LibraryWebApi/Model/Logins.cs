using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Model
{
    public class Logins
    {
        [Key]
        public int Id_login { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int Id_Reader { get; set; }
        //public Readers Readers { get; set; }
    }
}
