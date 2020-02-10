using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class AttorneyFirmType
    {
        [DataMember]
        public int AttorneyFirmTypeID { get; set; }
        [DataMember]
        public string AttorneyFirmTypeName { get; set; }
    }
}
