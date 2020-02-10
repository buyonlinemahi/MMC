using System;
using System.Runtime.Serialization;
namespace MMCService.DTO
{
    [DataContract]
    public class TreatmentCategory
    {
        [DataMember]
        public int TreatmentCategoryID { get; set; }
        [DataMember]
        public string TreatmentCategoryName { get; set; }
    }
}
