using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFIReport
    {
        public string RFAReportEmailTo { get; set; }
        public string RFAReportEmailCC { get; set; }
        public string RFAReportDocumentPath { get; set; }
        public string RFAReportMailContent { get; set; }
        public string RFAReportSubject { get; set; }
        public int RFAReferralFileID { get; set; }
        public int popUpID { get; set; }
    }
}
