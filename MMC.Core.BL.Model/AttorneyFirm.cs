
namespace MMC.Core.BL.Model
{
    public class AttorneyFirm
    {
        public int AttorneyFirmID { get; set; }
        public string AttorneyFirmName { get; set; }
        public int AttorneyFirmTypeID { get; set; }
        public string AttAddress1 { get; set; }
        public string AttAddress2 { get; set; }
        public string AttCity { get; set; }
        public int AttStateID { get; set; }
        public string AttZip { get; set; }
        public string AttPhone { get; set; }
        public string AttFax { get; set; }
        
        public string AttStateName { get; set; }
        public string AttorneyFirmType { get; set; }
    }
}
