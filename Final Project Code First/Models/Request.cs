using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("Request")]
    public class Request
    {

        [Key]
        public int Id { get; set; }
        [Column("Request_Status_Id")]
        public RequestStatusEnum RequestStatusId { get; set; }
        public RequestStaus RequestStaus { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateOfMessage { get; set; }
        public bool? Swap { get; set; }

        // Navigation
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        [ForeignKey("SenderUser")]
        [Column("Request_Sender_Id")]
        public int? SenderId { get; set; }
        public virtual User SenderUser { get; set; }
        [ForeignKey("RecieverUser")]
        [Column("Request_Reciever_Id")]
        public int? RecieverId { get; set; }
        public virtual User RecieverUser { get; set; }
    }
    public enum RequestStatusEnum
    {
        Requested = 0,
        RequestSwap = 1,
        Accepted = 2,
        AcceptSwap = 3,
        Refused = 4,
    }
    public class RequestStaus
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RequestStatusEnum Id { get; set; }
        [DataType("nvarchar(max)")]
        public string Name { get; set; }
    }
}