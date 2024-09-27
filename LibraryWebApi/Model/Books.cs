﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Model
{
    public class Books
    {

        [Key]
        public int Id_Book { get; set; }
        public string Author { get; set; }
        [Required]
        [ForeignKey("Genres")]
        public int Id_Genre { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }

        //public Genre Genre { get; set; }
    }
}