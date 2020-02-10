using MMCApp.Domain.Models.ThirdPartyAdministratorModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ThirdPartyAdministratorViewModel
{
    public class ThirdPartyAdministratorViewModel
    {
        public IEnumerable<ThirdPartyAdministrator> ThirdPartyAdministratorDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
