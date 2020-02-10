using MMCApp.Domain.Models.IMRModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IMRViewModel
{
    public class RequestIMRRecordViewModel
    {
        public IEnumerable<RequestIMRRecord> RequestIMRRecordDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
 