using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
   public class RFARequestBodyPart
   {
       [DataMember]
        public int RFARequestBodyPartID { get; set; }
         [DataMember]
        public int ClaimBodyPartID { get; set; }
         [DataMember]
        public string BodyPartType { get; set; }
         [DataMember]
        public int RFARequestID { get; set; }
    }
}
