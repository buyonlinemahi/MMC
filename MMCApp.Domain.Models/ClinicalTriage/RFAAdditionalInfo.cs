
namespace MMCApp.Domain.Models.ClinicalTriage
{
    public class RFAAdditionalInfo
    {
        public int RFAAdditionalInfoID { get; set; }
        public int RFAReferralID { get; set; }
        public string RFAAdditionalInfoRecord { get; set; }
        public System.DateTime? RFAAdditionalInfoRecordDate { get; set; }     
    }
}
