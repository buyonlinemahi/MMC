using System;

namespace MMC.Core.BL.Model
{
  public  class RequestHistory
    {
      public int RFARequestID { get; set; }
      public int RFAReferralID { get; set; }
      public string filename { get; set; }
      public DateTime UploadDate { get; set; }
      public int FileTypeId { get; set; }
      public string UserName { get; set; }
    }
}
