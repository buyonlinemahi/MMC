using MMCApp.Domain.Models.ClientModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.ClientModelViewModel
{
  public  class ClientEmployerViewModel
    {
      public IEnumerable<ClientEmployer> ClientEmployerDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
