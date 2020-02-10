using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RequestHistory
    {
        [DataMember]
        public int RFARequestID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public string filename { get; set; }
        [DataMember]
        public DateTime UploadDate { get; set; }
        [DataMember]
        public int FileTypeId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}
