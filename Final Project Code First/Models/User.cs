using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [DataType("nvarchar(Max)")]
        [Column("First_Name")]
        public string FirstName { get; set; }
        [Required]
        [DataType("nvarchar(Max")]
        [Column("Last_Name")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool Blocked { get; set; }
        [DataType("decimal(5,3)")]
        public decimal? Rate { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [Column("Phone_Number")]
        public double PhoneNumber { get; set; }
        [Column("Photo_Url")]
        public string PhotoUrl { get; set; }
        //Navigation
        [JsonIgnore]
        public virtual ICollection<UserHaveBook> UserHaveBooks { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserWantBook> UserWantBooks { get; set; }
        //public virtual ICollection<Book> UserWantBooks { get; set; }
        // public ICollection<UserWantBook> UserWantBooks { get; set; }
        [InverseProperty("SenderUser")]
        [JsonIgnore]

        public virtual ICollection<Report> SendedReports { get; set; }
        [InverseProperty("ReportedUser")]
        [JsonIgnore]

        public virtual ICollection<Report> RecievedReports { get; set; }
        [InverseProperty("ChatSenderUser")]
        [JsonIgnore]

        public virtual ICollection<Chat> SendedChat { get; set; }
        [InverseProperty("ChatRecieverUser")]
        [JsonIgnore]
        public virtual ICollection<Chat> RecievedChat { get; set; }
        [InverseProperty("SenderUser")]
        [JsonIgnore]

        public virtual ICollection<Request> SentRequests { get; set; }
        [InverseProperty("RecieverUser")]
        [JsonIgnore]

        public virtual ICollection<Request> RecievedRequests { get; set; }
        [InverseProperty("RateSenderUser")]
        public virtual ICollection<Rating>  SentedRates{ get; set; }
        [InverseProperty("RateRatedUser")]
        public virtual ICollection<Rating>  ReceivedRates { get; set; }
        [Required]
        [Column("User_Role_Id")]
        //[ForeignKey("User_Role")]
        public UserRole Role { get; set; }
        [ForeignKey("Role")]
        public virtual UserRoleTable UserRole { get; set; }


    }
    public enum UserRole
    {
        Admin = 0,
        User = 1
    }
    [Table("User_Role")]
    public class UserRoleTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public UserRole Id { get; set; }

        [Required]
        [DataType("nvarchar(max)")]
        public string Name { get; set; }
    }
}