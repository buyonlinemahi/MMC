using System.Runtime.Serialization;

namespace MMCService.DTO
{
    public class PatientClaimDiagnose
    {
        [DataMember]
        public int PatientClaimDiagnosisID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public int icdICDNumberID { get; set; }
        [DataMember]
        public string icdICDNumber { get; set; }
        [DataMember]
        public string icdICDTab { get; set; }
        [DataMember]
        public string ICDDescription { get; set; }
    }
}
