using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class CurrentWorkloadReport
    {
        [DataMember]
        public int CurrentWorkloadReportID { get; set; }
        [DataMember]
        public string CurrentWorkloadReportName { get; set; }
    }
}
