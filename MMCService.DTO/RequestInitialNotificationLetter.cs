using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO
{
    [DataContract]
    public class RequestInitialNotificationLetter
    {
        [DataMember]
        public int RequestID { get; set; }
        [DataMember]
        public string Treatment { get; set; }
        [DataMember]
        public int Frequency { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public string DurationType { get; set; }
        [DataMember]
        public string RequestType { get; set; }
        [DataMember]
        public string TreatmentType { get; set; }        
        [DataMember]
        public int? QTY { get; set; }
    }
}

