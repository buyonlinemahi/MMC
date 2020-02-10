using MMCApp.Domain.Models.ADRModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ADRViewModel
{
    public class ADRViewModel
    {
        public IEnumerable<ADR> ADRDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
