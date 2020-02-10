using System;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class RFAReferralFile
    {
        [DataMember]
        public int RFAReferralFileID { get; set; }
        [DataMember]
        public int RFAReferralID { get; set; }
        [DataMember]
        public int RFAFileTypeID { get; set; }
        [DataMember]
        public string RFAReferralFileName { get; set; }
        [DataMember]
        public string FileTypeName { get; set; }
        [DataMember]
        public DateTime? RFAFileCreationDate { get; set; }
        [DataMember]
        public int RFAFileUserID { get; set; }
        [DataMember]
        public string RFAType { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string Mode { get; set; }
    }
}