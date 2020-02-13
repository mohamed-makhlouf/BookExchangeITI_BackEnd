using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    public class Chat
    {
        [Key,Column("Chat_Id",Order = 0)]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        [Column("Date_Of_Message")]
        public DateTime DateOfMessage { get; set; }
        [Required]
        [Column("Chat_Status_Id")]
        public ChatStatusEnum ChatStatusId { get; set; }
        public virtual ChatStatus ChatStatus { get; set; }

       // [Required]
        //[Column("Chat_Sender_User_Id")]
        ////[ForeignKey("SenderUser")]
        //public int? SenderUserId { get; set; }
        public virtual User ChatSenderUser { get; set; }
      //  [Required]
        //[ForeignKey("RecieverUser")]
        //[Column("Chat_Reciever_User_Id")]
        //public int? RecieverUSerId { get; set; }
        public virtual User ChatRecieverUser { get; set; }

    }
    public enum ChatStatusEnum
    {
        Sending = 0,
        Delivered = 1,
        Seen = 2,
        Deleted = 3
    }
    [Table("Chat_Status")]
    public class ChatStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ChatStatusEnum Id { get; set; }
        [Required]
        [DataType("nvarchar(max)")]
        public string Name { get; set; }
    }
}