using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Vendor
    {
        [DataMember]
        public int VendorID { get; set; }
        [DataMember]
        public string VendorName { get; set; }
        [DataMember]
        public string VendorTax { get; set; }
        [DataMember]
        public string VendorAddress1 { get; set; }
        [DataMember]
        public string VendorAddress2 { get; set; }
        [DataMember]
        public string VendorCity { get; set; }
        [DataMember]
        public int VendorStateId { get; set; }
        [DataMember]
        public string VendorZip { get; set; }
        [DataMember]
        public string VendorPhone { get; set; }
        [DataMember]
        public string VendorFax { get; set; }
    }
} 
