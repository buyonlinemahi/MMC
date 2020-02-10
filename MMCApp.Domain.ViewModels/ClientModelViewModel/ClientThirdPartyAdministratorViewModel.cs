using MMCApp.Domain.Models.ClientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClientModelViewModel
{
  public class ClientThirdPartyAdministratorViewModel
    {
        public IEnumerable<ClientThirdPartyAdministrator> ClientTPADetails { get; set; }
        public int TotalCount { get; set; }
    }
}
