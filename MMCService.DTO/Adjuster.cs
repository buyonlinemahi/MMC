using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Adjuster
    {
        [DataMember]
        public int AdjusterID { get; set; }
        [DataMember]
        public string AdjFirstName { get; set; }
        [DataMember]
        public string AdjLastName { get; set; }
        [DataMember]
        public string AdjAddress1 { get; set; }
        [DataMember]
        public string AdjAddress2 { get; set; }
        [DataMember]
        public int AdjStateId { get; set; }
        [DataMember]
        public string AdjZip { get; set; }
        [DataMember]
        public string AdjPhone { get; set; }
        [DataMember]
        public string AdjFax { get; set; }
        [DataMember]
        public string AdjEMail { get; set; }
        [DataMember]
        public string AdjCity { get; set; }
        [DataMember]
        public string AdjStateName { get; set; }
        [DataMember]
        public int? ClientID { get; set; }
    }
}
