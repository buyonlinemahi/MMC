
namespace MMC.Core.BL.Model
{
  public  class RFAReferralFile
    {
        public int RFAReferralFileID { get; set; }
        public int RFAReferralID { get; set; }
        public int RFAFileTypeID { get; set; }
        public string RFAReferralFileName { get; set; }
        public string FileTypeName { get; set; }
        public System.DateTime? RFAFileCreationDate { get; set; }
        public string RFAType { get; set; }
        public string TableName { get; set; }
        public string Mode { get; set; }
    }
}
