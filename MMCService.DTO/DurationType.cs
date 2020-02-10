using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class DurationType
    {
        [DataMember]
        public int DurationTypeID { get; set; }
        [DataMember]
        public string DurationTypeName { get; set; }
    }
}
