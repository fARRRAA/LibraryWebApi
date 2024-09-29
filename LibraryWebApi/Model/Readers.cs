using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibraryWebApi.Model
{
    public class Readers
    {
        [Key]
        public int Id_User { get; set; }
        public string Name { get; set; }
        public DateTime Date_Birth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey("Roles")]
        public int? Id_Role { get; set; } = 2;

    }
}
