using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.CommonModel
{
    public class EmailMultipleAttachment
    {
        public string SendEMailTo { get; set; }
        public string SendEMailCc { get; set; }
        public IEnumerable<AttachementLinks> AttachmentForEmailArray { get; set; }
        public string Sendsubject { get; set; }
        public string SendEmailText { get; set; }
        public string popUpName { get; set; }
        public int rflID { get; set; }
    }
    public class AttachementLinks
    {
        public string AttachmentLink { get; set; }
    } 
}
