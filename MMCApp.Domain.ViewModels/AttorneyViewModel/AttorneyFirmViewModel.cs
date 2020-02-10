using MMCApp.Domain.Models.AttorneyModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.AttorneyViewModel
{
    public class AttorneyFirmViewModel
    {
        public IEnumerable<AttorneyFirm> AttorneyFirmDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
