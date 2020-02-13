using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    public enum BookConditionEnum
    {
        New = 0,
        Good = 1,
        Fair = 2,
        Old = 3
    }
    [Table("Book_Conditions")]
    public class BookCondition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public BookConditionEnum Id { get; set; }
        [Required]
        [DataType("nvarchar(max)")]
        public string Name { get; set; }
    }

}