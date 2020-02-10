using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RFADiagnosis
    {
        [DataMember]
        public string ICDDescription { get; set; }
        [DataMember]
        public string icdICDNumber { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int PatientClaimDiagnosisID { get; set; }
    }
}
