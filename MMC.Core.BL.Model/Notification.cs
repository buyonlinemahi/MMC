
namespace MMC.Core.BL.Model
{
    public class Notification
    {
        public int RFAReferralID { get; set; }
        public int RFARequestID { get; set; }
        public string PatFirstName { get; set; }
        public string PatLastName { get; set; }
        public string PatClaimNumber { get; set; }
        public string RFARequestedTreatment { get; set; }
    }
}
 