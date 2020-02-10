using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class FrequencyType
    {
        [DataMember]
        public int FrequencyTypeID { get; set; }
        [DataMember]
        public string FrequencyTypeName { get; set; }
    }
}