using System.Collections.Generic;
using MMC.Core.BL.Model.Base;
namespace MMC.Core.BL.Model.Paged
{
    public class EmailRecordAttachment : BasePaged
    {
        public IEnumerable<MMC.Core.BL.Model.EmailRecordAttachment> EmailRecordAttachmentDetails { get; set; }
    }
}
