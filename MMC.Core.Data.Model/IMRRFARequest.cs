
using System;
namespace MMC.Core.Data.Model
{
    public class IMRRFARequest
    {
        public int IMRRFARequestID { get; set; }
        public int RFARequestID { get; set; }
        public Boolean? Overturn { get; set; }
        public int? IMRApprovedUnits { get; set; }
    }
}
