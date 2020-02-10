using MMCApp.Domain.Models.ClaimStatusModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClaimStatusViewModel
{
    public class ClaimStatusViewModel
    {
        public IEnumerable<ClaimStatus> ClaimStatusDetails { get; set; }
    }
}
