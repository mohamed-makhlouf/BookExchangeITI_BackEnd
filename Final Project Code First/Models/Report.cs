using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Final_Project_Code_First.Models
{
    [Table("Report")]
    public class Report
    {
        [Key]
        [Column("Report_Id",Order = 0)]
        public int Id { get; set; }
        [DataType("nvarchar(max)")]
        [Required]
        public string Complain { get; set; }
      
       
        //[InverseProperty("SendedReports")]
        [ForeignKey("SenderUser")]
        [Column("Report_Sender_User_Id")]
        [Required]
        public int UserSenderId { get; set; }
        public virtual User SenderUser { get; set; }
        //[InverseProperty("RecievedReports")]
        [ForeignKey("ReportedUser")]
        [Column("Report_Reported_User_Id")]
        public int? UserReportedId { get; set; }
        public virtual User ReportedUser { get; set; }

    }
}