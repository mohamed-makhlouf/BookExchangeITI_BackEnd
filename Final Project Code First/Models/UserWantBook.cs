using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("User_Want_Book")]
    public class UserWantBook
    {
        [Key, Column("User_Id", Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Key, Column("Book_Id", Order = 1)]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        [Column("Date_Added")]
        public DateTime DateBookAdded { get; set; }
        //Navigation
        public User User { get; set; }
        public Book Book { get; set; }
    }
}