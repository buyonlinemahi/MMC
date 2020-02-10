using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Core.BL.Model
{
    public class IMRDecisionRequestDetails
    {
        public int RFARequestID { get; set; }
        public string RFARequestedTreatment { get; set; }
        public int RFAQuantity { get; set; }
        public int IMRRFARequestID { get; set; }
        public int? IMRApprovedUnits { get; set; }
        public Boolean? Overturn { get; set; }        
    }
}
