using MMCApp.Domain.Models.ClientModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.ClientModelViewModel
{
   public class ClientInsurerViewModel
    {
       public IEnumerable<ClientInsurer> ClientInsurerDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
