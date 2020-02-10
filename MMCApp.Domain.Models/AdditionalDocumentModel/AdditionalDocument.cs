using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.AdditionalDocumentModel
{
    public class AdditionalDocument
    {
        public string TypeName { get; set; }
        public int FileID { get; set; }
        public int RFAReferralID { get; set; }
        public string DocumentName { get; set; }
        public string RFAFileName { get; set; }
        public DateTime? DocumentDate { get; set; }
        public int PatientClaimID { get; set; }
        public bool IsChecked { get; set; }
    }
}
