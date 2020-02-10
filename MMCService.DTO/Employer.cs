using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Employer
    {
        [DataMember]
        public int EmployerID { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string EmpAddress1 { get; set; }
        [DataMember]
        public string EmpAddress2 { get; set; }
        [DataMember]
        public string EmpCity { get; set; }
        [DataMember]
        public int EmpStateId { get; set; }
        [DataMember]
        public string EmpZip { get; set; }
        [DataMember]
        public string EmpPhone { get; set; }
        [DataMember]
        public string EmpFax { get; set; }
        [DataMember]
        public string EmpEMail { get; set; }
        [DataMember]
        public string EmpContactName { get; set; }
        [DataMember]
        public string EmpPhoneExtension { get; set; }
        [DataMember]
        public string EmpStateName { get; set; }
    }
}
