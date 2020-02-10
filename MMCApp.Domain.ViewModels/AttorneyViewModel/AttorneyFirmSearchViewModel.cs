using MMCApp.Domain.Models.AttorneyModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.AttorneyViewModel
{
    public class AttorneyFirmSearchViewModel
    {
        public IEnumerable<Attorney> AttorneyDetails { get; set; }
        public AttorneyFirm AttorneyFirmDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
