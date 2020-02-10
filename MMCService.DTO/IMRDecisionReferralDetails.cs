using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class IMRDecisionReferralDetails
    {
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int IMRRFAReferralID { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public string PatClaimNumber { get; set; }
        [DataMember]
        public int PatientID { get; set; }
        [DataMember]
        public string PatFirstName { get; set; }
        [DataMember]
        public string PatLastName { get; set; }
        [DataMember]
        public DateTime DueDate { get; set; }
        [DataMember]
        public int IMRDecisionID { get; set; }    
        [DataMember]
        public DateTime? IMRDecisionReceivedDate { get; set; }
    }
}
