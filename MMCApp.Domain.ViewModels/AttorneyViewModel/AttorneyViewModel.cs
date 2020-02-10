using MMCApp.Domain.Models.AttorneyModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.AttorneyViewModel
{
    public class AttorneyViewModel
    {
        public IEnumerable<Attorney> AttorneyDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
