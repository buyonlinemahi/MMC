using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.Status, Schema = Constant.Constant.Schemas.lookup)]
   public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        
    }
}
