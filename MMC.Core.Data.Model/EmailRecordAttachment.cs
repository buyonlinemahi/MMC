using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Core.Data.Model
{
    public class EmailRecordAttachment
    {
        [Key]
        public int EmailAttachmentId { get; set; }
        public int EmailRecordId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }


        [ForeignKey("EmailRecordId")]
        public EmailRecord EmailRecords { get; set; }
    }
}
