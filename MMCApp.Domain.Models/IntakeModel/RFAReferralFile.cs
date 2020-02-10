using System;
using System.Web;
namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFAReferralFile
    {
        public int RFAReferralFileID { get; set; }
        public int RFAReferralID { get; set; }
        public int RFAFileTypeID { get; set; }
        public string RFAReferralFileName { get; set; }
        public string FileTypeName { get; set; }
        public DateTime? RFAFileCreationDate { get; set; }
        public int RFAFileUserID { get; set; }
        public HttpPostedFileBase rfaReferralFile { get; set; }
        public string RFAReferralFileFullPath { get; set; }
        public int TotalPages { get; set; }
        public int ProcessLevel { get; set; }
        public bool IsChecked { get; set; }
        public string Mode { get; set; }
        public string RFAType { get; set; }
        public string TableName { get; set; }
        public int Order { get; set; }
    }
}