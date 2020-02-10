using MMCApp.Domain.Models.ThirdPartyAdministratorBranchModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ThirdPartyAdministratorBranchViewModel
{
    public class ThirdPartyAdministratorBranchViewModel
    {
        public IEnumerable<ThirdPartyAdministratorBranch> ThirdPartyAdministratorBranchDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
