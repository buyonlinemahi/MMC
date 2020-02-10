using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMCService.DTO.Paged
{
    [DataContract]
    public class PagedEmployerSubsidiary
    {
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
        public IEnumerable<EmployerSubsidiary> EmployerSubsidiaryDetails { get; set; }
    }
}
