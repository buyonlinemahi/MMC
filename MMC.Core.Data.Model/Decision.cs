using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.Decision, Schema = Constant.Constant.Schemas.lookup)]
    public class Decision
    {
        public int DecisionID { get; set; }
        public string DecisionDesc { get; set; }
    }
}
