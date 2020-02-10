using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class RequestType
    {
        [DataMember]
        public int RequestTypeID { get; set; }
        [DataMember]
        public int RequestTypeName { get; set; }
        [DataMember]
        public string RequestTypeDesc { get; set; }
    }
}
