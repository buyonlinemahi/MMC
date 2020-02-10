using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
  public  class RequestHistoryViewModel
    {
      public IEnumerable<RequestHistory> RequestHistoryDetails { get; set; }
        public int TotalCount { get; set; }
        public int NotificationProcessLevelCheck { get; set; }
        public int CliniclAuditProcessLevelCheck { get; set; }
      
    }
}
