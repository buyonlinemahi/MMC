using MMCApp.Domain.Models.CaseManagerModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.CaseManagerViewModel
{
    public class CaseManagerSearchViewModel
    {
        public IEnumerable<CaseManager> CaseManagerDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
