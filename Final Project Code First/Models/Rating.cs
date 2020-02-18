using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Final_Project_Code_First.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        ////[Key]
        ////[ForeignKey("RateSenderUser")]
        //[Column("Sender_User_Id",Order =0)]
        //public int? SenderUserId { get; set; }

        public virtual User RateSenderUser { get; set; }

        [DataType("decimal(5,3)")]
        [Required]
        public decimal Rate { get; set; }
        //[Key]
        //[ForeignKey("RateRatedUser")]
        //[Column("Rated_User_Id",Order =1)]
        //public int? RatedUserId { get; set; }
        public virtual User RateRatedUser { get; set; }

    }
}