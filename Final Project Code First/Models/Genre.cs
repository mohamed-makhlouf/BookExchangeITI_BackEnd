using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("Genre")]   
    public class Genre
    {
        [Key]
        public int Genre_Id { get; set; }
        [DataType("nvarchar(Max)")]
      
        [Required]
        public string Genre_Name { get; set; }
        [JsonIgnore]
        public ICollection<Book> Books { get; set; }

    }
}