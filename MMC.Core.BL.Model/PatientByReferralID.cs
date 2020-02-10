using System;

namespace MMC.Core.BL.Model
{
    public class PatientByReferralID
    {
        public int RFAReferralID {get;set;}
        public string PatFirstName {get;set;}
        public string PatLastName {get;set;}
        public string PatClaimNumber {get;set;}
        public DateTime PatDOI { get; set; }
        public int PatientID { get; set; }
        public int PatientClaimID { get; set; }
        public int ClientBillingRateTypeID { get; set; }
    }
}
