using MMCApp.Domain.Models.ClientModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ClientModelViewModel
{
    public class ClientViewModel
    {
        public IEnumerable<Client> ClientDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
