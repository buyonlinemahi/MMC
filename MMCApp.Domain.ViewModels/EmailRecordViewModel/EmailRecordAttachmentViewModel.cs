using MMCApp.Domain.Models.EmailRecordModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.EmailRecordViewModel
{
    public class EmailRecordAttachmentViewModel
    {
        public IEnumerable<EmailRecordAttachment> EmailRecordAttachmentDetails { get; set; }
    }
}
