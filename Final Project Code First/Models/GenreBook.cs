using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("Genre_Book")]
    public class GenreBook
    {
        //[Key]
        [Column("Genre_Id",Order =0),Key]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]

        public Genre Genre { get; set; }
        [Column("Book_Id", Order = 1),Key]
        //[Key]
        public int BookId { get; set; }
        [ForeignKey("BookId")]

        public Book Book { get; set; }

    }
}