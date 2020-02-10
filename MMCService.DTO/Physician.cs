using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace MMCService.DTO
{
    [DataContract]
    public class Physician
    {
        [DataMember]
        public int PhysicianId { get; set; }
        [DataMember]
        public string PhysFirstName { get; set; }
        [DataMember]
        public string PhysLastName { get; set; }
        [DataMember]
        public string PhysQualification { get; set; }
        [DataMember]
        public int PhysSpecialtyId { get; set; }
        [DataMember]
        public string PhysNPI { get; set; }
        [DataMember]
        public string PhysEMail { get; set; }
        [DataMember]
        public string PhysNotes { get; set; }       
        [DataMember]
        public string PhysAddress1 { get; set; }
        [DataMember]
        public string PhysAddress2 { get; set; }
        [DataMember]
        public string PhysCity { get; set; }
        [DataMember]
        public int PhysStateId { get; set; }
        [DataMember]
        public string PhysZip { get; set; }
        [DataMember]
        public string PhysPhone { get; set; }
        [DataMember]
        public string PhysFax { get; set; }
        [DataMember]
        public string PhysStateName { get; set; }
        [DataMember]
        public string PhysSpecialtyName { get; set; }
    }
}
