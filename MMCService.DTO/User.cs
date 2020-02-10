using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserFirstName { get; set; }
        [DataMember]
        public string UserLastName { get; set; }
        [DataMember]
        public string UserPassword { get; set; }
        [DataMember]
        public string UserPhone { get; set; }
        [DataMember]
        public string UserFax { get; set; }
        [DataMember]
        public string UserEmail { get; set; }
        [DataMember]
        public string UserSignatureFileName { get; set; }
        [DataMember]
        public bool? UserDeletePermission { get; set; }
        [DataMember]
        public bool? UserManagementPermission { get; set; }  
    }
}
