using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedPatientHistory
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<PatientHistory> PatientHistoryDetails { get; set; }
    }
}
