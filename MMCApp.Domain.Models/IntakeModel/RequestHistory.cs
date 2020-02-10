using System;

namespace MMCApp.Domain.Models.IntakeModel
{
  public  class RequestHistory
    {
        public int RFARequestID { get; set; }
        public int RFAReferralID { get; set; }
        public string filename { get; set; }
        public DateTime UploadDate { get; set; }
        public int FileTypeId { get; set; }
        public string UserName { get; set; }
        public string FileFullPath { get; set; }
        public bool IsChecked { get; set; }
        public bool IsCheckedAll { get; set; }
    }
}
