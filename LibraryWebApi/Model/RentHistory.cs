﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWebApi.Model
{
    public class RentHistory
    {
        [Key]
        public int id_Rent { get; set; }
        public DateTime Rental_Start { get; set; }
        public int Rental_Time { get; set; }


        [ForeignKey("Readers")]
        public int Id_Reader { get; set; }
        [NotMapped]
        public Readers Reader {  get; set; }


        [ForeignKey(name: "Books")]
        public int Id_Book { get; set; }
        [NotMapped]
        public Books Book { get; set; }
        public DateTime Rental_End { get; set; }
        public string Rental_Status {  get; set; }


    }
}
