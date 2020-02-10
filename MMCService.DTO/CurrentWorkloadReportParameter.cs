using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class CurrentWorkloadReportParameter
    {
        [DataMember]
        public int CurrentWorkloadReportParameterID { get; set; }
        [DataMember]
        public int? ClientID { get; set; }
        [DataMember]
        public int CurrentWorkloadReportID { get; set; }
        [DataMember]
        public int? StatusID { get; set; }
    }
}
