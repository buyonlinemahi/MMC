using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
   public class Insurer
   {
       [DataMember]
        public int InsurerID { get; set; }
        [DataMember]
        public string InsName { get; set; }
        [DataMember]
        public string InsAddress1 { get; set; }
        [DataMember]
        public string InsAddress2 { get; set; }
        [DataMember]
        public string InsCity { get; set; }
        [DataMember]
        public int InsStateId { get; set; }
        [DataMember]
        public string InsZip { get; set; }
        [DataMember]
        public string InsPhone { get; set; }
        [DataMember]
        public string InsFax { get; set; }
        [DataMember]
        public string InsEMail { get; set; }
        [DataMember]
        public string InsContactName { get; set; }
        [DataMember]
        public string InsPhoneExtension { get; set; }
        [DataMember]
        public string InsStateName { get; set; }
    }
}
