using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MMCService.DTO
{
    [DataContract]
    public class Language
    {
        [DataMember]
        public int LanguageID { get; set; }
        [DataMember]
        public string LanguageName { get; set; }
    }
}
