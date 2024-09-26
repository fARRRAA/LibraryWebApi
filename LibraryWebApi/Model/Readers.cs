using System.ComponentModel.DataAnnotations;
namespace LibraryWebApi.Model
{
    public class Readers
    {
        [Key]
        public int Id_User { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }    

    }
}
