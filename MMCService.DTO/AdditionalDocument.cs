using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class AdditionalDocument
    {
        [DataMember]
        public string TypeName { get; set; }
        [DataMember]
        public int FileID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string RFAFileName { get; set; }
        [DataMember]
        public DateTime? DocumentDate { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; } 
    }
}
