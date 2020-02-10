using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class PatientOccupational
    {
        [DataMember]
        public int PatientOccupationalID { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatOptJobTitle { get; set; }
        [DataMember]
        public string PatOptJobDescription { get; set; }
        [DataMember]
        public string PatOptInjuryType { get; set; }
        [DataMember]
        public string PatOptInjuryDescription { get; set; }
    }
}
