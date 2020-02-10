using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Core.BL.Model
{
    public class EmailRecordAttachment
    {
        public int EmailAttachmentId { get; set; }
        public int EmailRecordId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string EmRecTo { get; set; }
        public string EmRecCC { get; set; }
        public string EmRecSubject { get; set; }
        public string EmRecBody { get; set; }
    }
}
