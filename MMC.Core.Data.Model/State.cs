using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.State, Schema = Constant.Constant.Schemas.lookup)]
    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
