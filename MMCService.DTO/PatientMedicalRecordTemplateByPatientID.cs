using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class PatientMedicalRecordTemplateByPatientID
    {
        [DataMember]
        public string PatientName { get; set; }
        [DataMember]
        public string Diagnosis { get; set; }
        [DataMember]
        public string Claims { get; set; }
        [DataMember]
        public string AcceptedBodyParts { get; set; }
        [DataMember]
        public string DeniedBodyParts { get; set; }
        [DataMember]
        public string DiscoveryBodyParts { get; set; }
    }
}
