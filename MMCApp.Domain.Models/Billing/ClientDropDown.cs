using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.Billing
{
    public class ClientDropDown
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
    }
    public class ClientDropDownList
    {
        public IEnumerable<ClientDropDown> ClientNameDetails { get; set; }
    }
}
