using MMCApp.Domain.Models.ClientModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.ClientModelViewModel
{
    public class ClientManagedCareCompanyViewModel
    {
        public IEnumerable<ClientManagedCareCompany> ClientMCCDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
