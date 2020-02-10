
namespace MMC.Core.BL.Model
{
    public class PatientHistory
    {
        public int PatientID { get; set; }
        public int RFAReferralID { get; set; }
        public int RFARequestID { get; set; }
        public System.DateTime? RFARequestDate { get; set; }
        public string Physician { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int? PhysicianId { get; set; }
        public System.DateTime? DecisionDate { get; set; }
        public string Status { get; set; }
    }
}
