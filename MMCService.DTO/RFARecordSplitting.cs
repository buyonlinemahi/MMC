using System;
using System.Runtime.Serialization;


namespace MMCService.DTO
{
    [DataContract]
    public class RFARecordSplitting
    {
        [DataMember]
        public int RFARecSpltID { get; set; }
        [DataMember]
        public int? RFAReferralID { get; set; }
        [DataMember]
        public int DocumentCategoryID { get; set; }
        [DataMember]
        public int DocumentTypeID { get; set; }
        [DataMember]
        public string RFARecDocumentName { get; set; }
        [DataMember]
        public DateTime RFARecDocumentDate { get; set; }
        [DataMember]
        public int RFARecPageStart { get; set; }
        [DataMember]
        public int RFARecPageEnd { get; set; }
        [DataMember]
        public int PatientClaimID { get; set; }
        [DataMember]
        public string RFARecSpltSummary { get; set; }
        [DataMember]
        public string AuthorName { get; set; }
        [DataMember]
        public string RFAFileName { get; set; }
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public DateTime? RFAUploadDate { get; set; }
        [DataMember]
        public int? RFARequestID { get; set; }
    }
}
