using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{   
    [Table("Book")]
    public class Book
    {
        public Book()
        {
            //UserWantBooks = new HashSet<UserWantBook>();
            Categories = new List<string>();
        }
        [Key]
        public int Book_Id { get; set; }
        [DataType("nvarchar(MAX)")]
        [Required]
        public string Title { get; set; }
        [DataType("decimal(5,3)")]
        //[Required]
        public decimal? Rate { get; set; }
        [DataType(DataType.ImageUrl)]
        [Required]
        public string Photo_Url { get; set; }
        [DataType("nvarchar(max)")]
        [Required]
        public string Author_Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        //Navigation
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<GenreBook> Genres2 { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserHaveBook> UserHaveBooks { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserWantBook> UserWantBooks { get; set; }
        //[InverseProperty("UserWantBooks")]
        //[JsonIgnore]
        //public virtual ICollection<User> UserWantBooks { get; set; }
        [InverseProperty("SendedBook")]
        public virtual ICollection<Request> SendedRequests { get; set; }
        [InverseProperty("RequestedBook")]
        public virtual ICollection<Request> RecievedRequests { get; set; }


        //parameter
        [NotMapped]
        [Required]
        public String Want { get; set; }
        [NotMapped]
        public IList<string> Categories { get; set; }
    }
}