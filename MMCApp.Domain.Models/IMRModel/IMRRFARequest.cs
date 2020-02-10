using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.IMRModel
{
    public class IMRRFARequest
    {
        public int IMRRFARequestID { get; set; }
        public int RFARequestID { get; set; }
        public Boolean? Overturn { get; set; }
        public int? IMRApprovedUnits { get; set; }        
    }
}
