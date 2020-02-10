using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace MMCService.DTO
{
    [DataContract]
    public class Specialty
    {
        [DataMember]
        public int SpecialtyID { get; set; }
        [DataMember]
        public string SpecialtyName { get; set; }
    }
}
