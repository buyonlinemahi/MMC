using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFARequestBodyPart
    {
        public int RFARequestBodyPartID { get; set; }
        public int ClaimBodyPartID { get; set; }
        public string BodyPartType { get; set; }
        public int RFARequestID { get; set; }
    }
}
