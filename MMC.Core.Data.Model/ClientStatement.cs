using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Core.Data.Model
{
   public class ClientStatement
    {
          public int ClientStatementID { get; set; }
          public int ClientID { get; set; }
          public string ClientStatementNumber { get; set; }
          public DateTime? CreationDate { get; set; }
          public string ClientStatementFileName { get; set; }
          public int UserID { get; set; }
    }
}
