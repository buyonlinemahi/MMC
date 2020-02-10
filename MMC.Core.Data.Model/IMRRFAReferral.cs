using System;
namespace MMC.Core.Data.Model
{
    public class IMRRFAReferral
    {
        public int IMRRFAReferralID { get; set; }
        public int RFAReferralID { get; set; }
        public int IMRRFAClaimPhysicianID { get; set; }
        public DateTime IMRApplicationReceivedDate { get; set; }
        public DateTime IMRNoticeInformationDate { get; set; }

        public DateTime? IMRCEReceivedDate { get; set; }
        public DateTime? IMRInternalReceivedDate { get; set; }
        public int? IMRReceivedVia { get; set; }
        public DateTime? IMRResponseDueDate { get; set; }
        public int? IMRPriority { get; set; }
        public int? IMRResponseType { get; set; }
        public int? IMRDecisionID { get; set; }
        public DateTime? IMRResponseDate { get; set; }
        public DateTime? IMRDecisionReceivedDate { get; set; }
       
    }
}
